#include "DebugObject.h"


#if defined(EAE6320_DEBUG_SHAPES_AREENABLED)
#include "../../Engine/Asserts/Asserts.h"
#include "../../Engine/Logging/Logging.h"
#include "../../Engine/Graphics/Mesh.h"
#include "../../Engine/Graphics/Material.h"
#include "../../Engine/Graphics/MeshData.h"
#include "../../Engine/Math/Functions.h"
#include "../../Engine/Graphics/Graphics.h"

#include <cmath>

namespace
{
	const char* const materailPath = "data/materials/primitive_material.binmaterial";
}

namespace eae6320
{
	namespace Debug
	{
		namespace Shapes
		{
			Graphics::Material* DebugObject::ms_material = nullptr;
			std::vector<DebugObject*> DebugObject::ms_debugObjects;
#pragma region Sets
			void DebugObject::SetPosition(const Math::cVector i_position)
			{
				m_position = i_position;
			}
			void DebugObject::SetIsDisplayed(const bool i_isDisplayed)
			{
				m_isDisplayed = i_isDisplayed;
			}
#pragma endregion 

#pragma region Gets
			Graphics::Material* DebugObject::GetMaterial()
			{
				return ms_material;
			}
			Graphics::Mesh* DebugObject::GetMesh()const
			{
				return m_mesh;
			}
			Math::cVector DebugObject::GetPosition() const
			{
				return m_position;
			}
			bool DebugObject::GetIsDisplayed() const
			{
				return m_isDisplayed;
			}
			void DebugObject::GetColor(float& o_r, float& o_g, float& o_b)const
			{
				o_r = m_color.r;
				o_g = m_color.g;
				o_b = m_color.b;
			}
#pragma endregion 
			DebugObject::DebugObject(const bool i_isDisplayed,const Math::cVector i_position, const Color i_color) :
				m_type(),
				m_position(i_position),
				m_color(i_color),
				m_mesh(new Graphics::Mesh()),
				m_meshData(nullptr),
				m_isDisplayed(i_isDisplayed)
			{
				if (!ms_material)
				{
					ms_material = new Graphics::Material();
					Graphics::Material::LoadMaterial(materailPath, *ms_material);
				}
			}
			DebugObject::~DebugObject()
			{
				if (m_mesh)
				{
					delete m_mesh;
					m_mesh = nullptr;
				}
				if (m_meshData)
				{
					delete m_meshData;
					m_meshData = nullptr;
				}
				if (ms_material)
				{
					delete ms_material;
					ms_material = nullptr;
				}
			}

			void DebugObject::Draw(const bool i_ison)
			{
				if (!i_ison)return;
				const size_t length = ms_debugObjects.size();
				for (size_t i = 0; i < length; i++)
				{
					Graphics::SetDebugObject(ms_debugObjects[i]);
				}
			}

			void DebugObject::CreateBox(const float i_width, const float i_height, const float i_depth)
			{
				EAE6320_ASSERTF(i_width > 0.0f, "Specified width: %f is negative", i_width);
				EAE6320_ASSERTF(i_height > 0.0f, "Specified height: %f is negative", i_height);
				EAE6320_ASSERTF(i_depth > 0.0f, "Specified depth: %f is negative", i_depth);

				m_type = Shapes::BOX;

				const float halfWidth = 0.5f*i_width;
				const float halfHeight = 0.5f*i_height;
				const float halfDepth = 0.5f*i_depth;

				const float signs[24][3] =
				{
					// front
					{ -1.0f, -1.0f, -1.0f },
					{ -1.0f, +1.0f, -1.0f },
					{ +1.0f, +1.0f, -1.0f },
					{ +1.0f, -1.0f, -1.0f },
					//back
					{ -1.0f, -1.0f, +1.0f },
					{ +1.0f, -1.0f, +1.0f },
					{ +1.0f, +1.0f, +1.0f },
					{ -1.0f, +1.0f, +1.0f },
					//top
					{ -1.0f, +1.0f, -1.0f },
					{ -1.0f, +1.0f, +1.0f },
					{ +1.0f, +1.0f, +1.0f },
					{ +1.0f, +1.0f, -1.0f },
					//bottom
					{ -1.0f, -1.0f, -1.0f },
					{ +1.0f, -1.0f, -1.0f },
					{ +1.0f, -1.0f, +1.0f },
					{ -1.0f, -1.0f, +1.0f },
					//left
					{ -1.0f, -1.0f, +1.0f },
					{ -1.0f, +1.0f, +1.0f },
					{ -1.0f, +1.0f, -1.0f },
					{ -1.0f, -1.0f, -1.0f },
					//right
					{ +1.0f, -1.0f, -1.0f },
					{ +1.0f, +1.0f, -1.0f },
					{ +1.0f, +1.0f, +1.0f },
					{ +1.0f, -1.0f, +1.0f },
				};

				m_meshData = new  Graphics::MeshData(16, 24, 36);
				for (size_t i = 0; i < m_meshData->numberOfVertices; i++)
				{
					m_meshData->vertexData[i].AddVertexData(signs[i][0] * halfWidth, signs[i][1] * halfHeight, signs[i][2] * halfDepth);
				}

				const uint16_t indices[36] =
				{
					0,1,2,0,2,3,
					4,5,6,4,6,7,
					8,9,10,8,10,11,
					12,13,14,12,14,15,
					16,17,18,16,18,19,
					20,21,22,20,22,23
				};
				for (size_t i = 0; i < m_meshData->numberOfIndices; i++)
				{
#if defined( EAE6320_PLATFORM_D3D )
					reinterpret_cast<uint16_t*>(m_meshData->indexData)[m_meshData->numberOfIndices - 1 - i] = indices[i];
#elif defined( EAE6320_PLATFORM_GL )
					reinterpret_cast<uint16_t*>(m_meshData->indexData)[i] = indices[i];
#endif
				}

				if (!m_mesh->Initialize(*m_meshData))
				{
					EAE6320_ASSERT(false);
					Logging::OutputError("Failed to initialize debug box shape with %f width,%f height,%f depth", i_width, i_height, i_depth);
				}
			}
			void DebugObject::CreateLine(const Math::cVector i_end)
			{
				m_type = Shapes::LINE;
				m_meshData = new Graphics::MeshData(16, 3, 3);

				m_meshData->vertexData[0].AddVertexData(m_position.x, m_position.y, m_position.z);
				m_meshData->vertexData[1].AddVertexData(i_end.x, i_end.y, i_end.z);
				m_meshData->vertexData[2].AddVertexData(m_position.x, m_position.y, m_position.z);

				/*m_meshData->vertexData[0].AddVertexData(0.0f, 0.0f, 0.0f);
				m_meshData->vertexData[1].AddVertexData(i_end.x, i_end.y, i_end.z);
				m_meshData->vertexData[2].AddVertexData(0.0f, 0.0f, 0.0f);*/

				reinterpret_cast<uint16_t*>(m_meshData->indexData)[0] = 0;
				reinterpret_cast<uint16_t*>(m_meshData->indexData)[1] = 1;
				reinterpret_cast<uint16_t*>(m_meshData->indexData)[2] = 2;

				if (!m_mesh->Initialize(*m_meshData))
				{
					EAE6320_ASSERT(false);
					Logging::OutputError("Failed to initialize debug line");
				}
			}
			void DebugObject::CreateSphere(const float i_radius, const uint32_t i_sliceCount, const uint32_t i_stackCount)
			{
				EAE6320_ASSERTF(i_radius > 0.0f, "Specified radius: %f is negative", i_radius);
				EAE6320_ASSERTF(i_sliceCount > 0, "Specified slice count: %d is negative", i_sliceCount);
				EAE6320_ASSERTF(i_stackCount > 0, "Specified stack count: %d is negative", i_stackCount);

				m_type = Shapes::SPHERE;

				const float phiStep = Math::Pi / i_stackCount;
				const float thetaStep = 2.0f * Math::Pi / i_sliceCount;
				const int numberOfVertices = ((i_stackCount - 1)*(i_sliceCount + 1)) + 2;
				const int numberOfIndices = (i_sliceCount * 3) + (((i_stackCount - 2) * i_sliceCount) * 6) + (i_sliceCount * 3);
				int typeOfIndexData;
				if (numberOfIndices > USHRT_MAX)
				{
					typeOfIndexData = 32;
				}
				else
				{
					typeOfIndexData = 16;
				}
				m_meshData = new Graphics::MeshData(typeOfIndexData, numberOfVertices, numberOfIndices);
				// Filling Vertex Data
				{
					size_t index = 1;
					m_meshData->vertexData[0].AddVertexData(0.0f, i_radius, 0.0f);
					for (size_t i = 1; i <= i_stackCount - 1; i++)
					{
						float phi = i*phiStep;
						for (size_t j = 0; j <= i_sliceCount; j++)
						{
							float theta = j*thetaStep;

							float x = i_radius*sinf(phi)*cosf(theta);
							float y = i_radius*cosf(phi);
							float z = i_radius* sinf(phi)*sinf(theta);

							m_meshData->vertexData[index].AddVertexData(x, y, z);
							++index;
						}
					}
					m_meshData->vertexData[index].AddVertexData(0.0f, -i_radius, 0.0f);
				}
				//Filling Index Data
				{
					void * indices = nullptr;
					if (typeOfIndexData == 16)
					{
						indices = malloc(numberOfIndices * sizeof(uint16_t));
					}
					else
					{
						indices = malloc(numberOfIndices * sizeof(uint32_t));
					}
					size_t index = 0;
					for (size_t i = 1; i <= i_sliceCount; i++)
					{
						if (typeOfIndexData == 16)
						{
							reinterpret_cast<uint16_t*>(indices)[index] = 0;
							++index;
							reinterpret_cast<uint16_t*>(indices)[index] = static_cast<uint16_t>(i + 1);
							++index;
							reinterpret_cast<uint16_t*>(indices)[index] = static_cast<uint16_t>(i);
							++index;
						}
						else
						{
							reinterpret_cast<uint32_t*>(indices)[index] = 0;
							++index;
							reinterpret_cast<uint32_t*>(indices)[index] = static_cast<uint32_t>(i + 1);
							++index;
							reinterpret_cast<uint32_t*>(indices)[index] = static_cast<uint32_t>(i);
							++index;
						}
					}
					int baseIndex = 1;
					const int ringVertexCount = i_sliceCount + 1;
					for (size_t i = 0; i < i_stackCount - 2; i++)
					{
						for (size_t j = 0; j < i_sliceCount; j++)
						{
							if (typeOfIndexData == 16)
							{
								reinterpret_cast<uint16_t*>(indices)[index] = static_cast<uint16_t>(baseIndex + i*ringVertexCount + j);
								++index;
								reinterpret_cast<uint16_t*>(indices)[index] = static_cast<uint16_t>(baseIndex + i*ringVertexCount + j + 1);
								++index;
								reinterpret_cast<uint16_t*>(indices)[index] = static_cast<uint16_t>(baseIndex + (i + 1)*ringVertexCount + j);
								++index;

								reinterpret_cast<uint16_t*>(indices)[index] = static_cast<uint16_t>(baseIndex + (i + 1)*ringVertexCount + j);
								++index;
								reinterpret_cast<uint16_t*>(indices)[index] = static_cast<uint16_t>(baseIndex + i*ringVertexCount + j + 1);
								++index;
								reinterpret_cast<uint16_t*>(indices)[index] = static_cast<uint16_t>(baseIndex + (i + 1)*ringVertexCount + j + 1);
								++index;
							}
							else
							{
								reinterpret_cast<uint32_t*>(indices)[index] = static_cast<uint32_t>(baseIndex + i*ringVertexCount + j);
								++index;
								reinterpret_cast<uint32_t*>(indices)[index] = static_cast<uint32_t>(baseIndex + i*ringVertexCount + j + 1);
								++index;
								reinterpret_cast<uint32_t*>(indices)[index] = static_cast<uint32_t>(baseIndex + (i + 1)*ringVertexCount + j);
								++index;

								reinterpret_cast<uint32_t*>(indices)[index] = static_cast<uint32_t>(baseIndex + (i + 1)*ringVertexCount + j);
								++index;
								reinterpret_cast<uint32_t*>(indices)[index] = static_cast<uint32_t>(baseIndex + i*ringVertexCount + j + 1);
								++index;
								reinterpret_cast<uint32_t*>(indices)[index] = static_cast<uint32_t>(baseIndex + (i + 1)*ringVertexCount + j + 1);
								++index;
							}
						}
					}
					const int southPoleIndex = numberOfVertices - 1;
					baseIndex = southPoleIndex - ringVertexCount;
					for (size_t i = 0; i < i_sliceCount; i++)
					{
						if (typeOfIndexData == 16)
						{
							reinterpret_cast<uint16_t*>(indices)[index] = static_cast<uint16_t>(southPoleIndex);
							++index;
							reinterpret_cast<uint16_t*>(indices)[index] = static_cast<uint16_t>(baseIndex + i);
							++index;
							reinterpret_cast<uint16_t*>(indices)[index] = static_cast<uint16_t>(baseIndex + i + 1);
							++index;
						}
						else
						{
							reinterpret_cast<uint32_t*>(indices)[index] = static_cast<uint32_t>(southPoleIndex);
							++index;
							reinterpret_cast<uint32_t*>(indices)[index] = static_cast<uint32_t>(baseIndex + i);
							++index;
							reinterpret_cast<uint32_t*>(indices)[index] = static_cast<uint32_t>(baseIndex + i + 1);
							++index;
						}
					}
					for (size_t i = 0; i < m_meshData->numberOfIndices; i++)
					{
#if defined( EAE6320_PLATFORM_D3D )
						if (typeOfIndexData == 16)
						{
							reinterpret_cast<uint16_t*>(m_meshData->indexData)[m_meshData->numberOfIndices - 1 - i] = reinterpret_cast<uint16_t*>(indices)[i];
						}
						else
						{
							reinterpret_cast<uint32_t*>(m_meshData->indexData)[m_meshData->numberOfIndices - 1 - i] = reinterpret_cast<uint32_t*>(indices)[i];
						}
#elif defined( EAE6320_PLATFORM_GL )
						if (typeOfIndexData == 16)
						{
							reinterpret_cast<uint16_t*>(m_meshData->indexData)[i] = reinterpret_cast<uint16_t*>(indices)[i];
						}
						else
						{
							reinterpret_cast<uint32_t*>(m_meshData->indexData)[i] = reinterpret_cast<uint32_t*>(indices)[i];
						}
#endif
					}
					if (indices)
					{
						free(indices);
						indices = nullptr;
					}
				}
				if (!m_mesh->Initialize(*m_meshData))
				{
					EAE6320_ASSERT(false);
					Logging::OutputError("Failed to initialize debug line");
				}
			}

			void DebugObject::UpdateSphere(const float i_radius)
			{
				if (m_mesh)
				{
					delete m_mesh;
					m_mesh = nullptr;
				}
				m_mesh = new Graphics::Mesh();

				const float previousRadius = m_meshData->vertexData[0].y;
				const float deltaRadius = i_radius / previousRadius;

				for (size_t i = 0; i < m_meshData->numberOfVertices; i++)
				{
					m_meshData->vertexData[i].x *= deltaRadius;
					m_meshData->vertexData[i].y *= deltaRadius;
					m_meshData->vertexData[i].z *= deltaRadius;
				}

				if (!m_mesh->Initialize(*m_meshData))
				{
					EAE6320_ASSERT(false);
					Logging::OutputError("Failed to update debug sphere");
				}
			}

			void DebugObject::UpdateLine(const Math::cVector i_newEnd)
			{
				if (m_mesh)
				{
					delete m_mesh;
					m_mesh = nullptr;
				}
				m_mesh = new Graphics::Mesh();

				m_meshData->vertexData[0].AddVertexData(m_position.x, m_position.y, m_position.z);
				m_meshData->vertexData[1].AddVertexData(i_newEnd.x, i_newEnd.y, i_newEnd.z);
				m_meshData->vertexData[2].AddVertexData(m_position.x, m_position.y, m_position.z);

				reinterpret_cast<uint16_t*>(m_meshData->indexData)[0] = 0;
				reinterpret_cast<uint16_t*>(m_meshData->indexData)[1] = 1;
				reinterpret_cast<uint16_t*>(m_meshData->indexData)[2] = 2;

				if (!m_mesh->Initialize(*m_meshData))
				{
					EAE6320_ASSERT(false);
					Logging::OutputError("Failed to update debug line");
				}
			}

			void DebugObject::CreateCylinder(const float i_bottomRadius, const float i_topRadius, const float i_height, const uint32_t i_sliceCount, const uint32_t i_stackCount)
			{
				EAE6320_ASSERTF(i_bottomRadius > 0.0f, "Specified bottom radius: %f is negative", i_bottomRadius);
				EAE6320_ASSERTF(i_topRadius > 0, "Specified top radius: %f is negative", i_topRadius);
				EAE6320_ASSERTF(i_height > 0, "Specified height: %f is negative", i_height);
				EAE6320_ASSERTF(i_sliceCount > 0, "Specified slice count: %d is negative", i_sliceCount);
				EAE6320_ASSERTF(i_stackCount > 0, "Specified stack count: %d is negative", i_stackCount);

				m_type = Shapes::CYLINDER;

				const int numberOfVertices = ((i_stackCount + 1)*(i_sliceCount + 1)) + (i_sliceCount + 1) + (i_sliceCount + 1) + 2;
				const int numberOfIndices = (i_stackCount * i_sliceCount * 6) + (i_sliceCount * 3) + (i_sliceCount * 3);
				int typeOfIndexData;
				if (numberOfIndices > USHRT_MAX)
				{
					typeOfIndexData = 32;
				}
				else
				{
					typeOfIndexData = 16;
				}
				m_meshData = new Graphics::MeshData(typeOfIndexData, numberOfVertices, numberOfIndices);
				void * indices = nullptr;
				if (typeOfIndexData == 16)
				{
					indices = malloc(numberOfIndices * sizeof(uint16_t));
				}
				else
				{
					indices = malloc(numberOfIndices * sizeof(uint32_t));
				}
				size_t vertices_index = 0;
				size_t indices_index = 0;
				//Creating the middle part
				{
					// Filling Vertex Data
					{
						const float stackHeight = i_height / i_stackCount;
						const float radiusStep = (i_topRadius - i_bottomRadius) / i_stackCount;
						const uint32_t ringCount = i_stackCount + 1;

						for (size_t i = 0; i < ringCount; i++)
						{
							float y = -0.5f*i_height + i*stackHeight;
							float r = i_bottomRadius + i*radiusStep;
							float dTheta = 2.0f*Math::Pi / i_sliceCount;
							for (size_t j = 0; j <= i_sliceCount; j++)
							{
								float c = cosf(j*dTheta);
								float s = sinf(j*dTheta);

								float x = r*c;
								float z = r*s;

								m_meshData->vertexData[vertices_index].AddVertexData(x, y, z);
								++vertices_index;
							}
						}
					}
					// Filling Index Data
					{
						const int ringVertexCount = i_sliceCount + 1;
						for (size_t i = 0; i < i_stackCount; i++)
						{
							for (size_t j = 0; j < i_sliceCount; j++)
							{
								if (typeOfIndexData == 16)
								{
									reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>(i*ringVertexCount + j);
									++indices_index;
									reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>((i + 1)*ringVertexCount + j);
									++indices_index;
									reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>((i + 1)*ringVertexCount + j + 1);
									++indices_index;

									reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>(i*ringVertexCount + j);
									++indices_index;
									reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>((i + 1)*ringVertexCount + j + 1);
									++indices_index;
									reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>(i*ringVertexCount + j + 1);
									++indices_index;
								}
								else
								{
									reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>(i*ringVertexCount + j);
									++indices_index;
									reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>((i + 1)*ringVertexCount + j);
									++indices_index;
									reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>((i + 1)*ringVertexCount + j + 1);
									++indices_index;

									reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>(i*ringVertexCount + j);
									++indices_index;
									reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>((i + 1)*ringVertexCount + j + 1);
									++indices_index;
									reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>(i*ringVertexCount + j + 1);
									++indices_index;
								}
							}
						}
					}
				}

				//Creating top cap
				{
					size_t baseIndex;
					// Filling Vertex Data
					{
						baseIndex = vertices_index;

						float y = 0.5f*i_height;
						float dTheta = 2.0f*Math::Pi / i_sliceCount;

						for (size_t i = 0; i <= i_sliceCount; i++)
						{
							float x = i_topRadius*cosf(i*dTheta);
							float z = i_topRadius*sinf(i*dTheta);

							m_meshData->vertexData[vertices_index].AddVertexData(x, y, z);
							++vertices_index;
						}
						m_meshData->vertexData[vertices_index].AddVertexData(0.0f, y, 0.0f);
						++vertices_index;
					}
					// Filling Index Data
					{
						const size_t centerIndex = vertices_index - 1;
						for (size_t i = 0; i < i_sliceCount; i++)
						{
							if (typeOfIndexData == 16)
							{
								reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>(centerIndex);
								++indices_index;
								reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>(baseIndex + i + 1);
								++indices_index;
								reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>(baseIndex + i);
								++indices_index;
							}
							else
							{
								reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>(centerIndex);
								++indices_index;
								reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>(baseIndex + i + 1);
								++indices_index;
								reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>(baseIndex + i);
								++indices_index;
							}
						}
					}
				}

				//Creating bottom cap
				{
					size_t baseIndex;
					// Filling Vertex Data
					{
						baseIndex = vertices_index;

						float y = -0.5f * i_height;
						float dTheta = 2.0f*Math::Pi / i_sliceCount;

						for (size_t i = 0; i <= i_sliceCount; i++)
						{
							float x = i_bottomRadius * cosf(i * dTheta);
							float z = i_bottomRadius * sinf(i * dTheta);

							m_meshData->vertexData[vertices_index].AddVertexData(x, y, z);
							++vertices_index;
						}
						m_meshData->vertexData[vertices_index].AddVertexData(0.0f, y, 0.0f);
						++vertices_index;
					}
					// Filling Index Data
					{
						const size_t centerIndex = vertices_index - 1;
						for (size_t i = 0; i < i_sliceCount; i++)
						{
							if (typeOfIndexData == 16)
							{
								reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>(centerIndex);
								++indices_index;
								reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>(baseIndex + i);
								++indices_index;
								reinterpret_cast<uint16_t*>(indices)[indices_index] = static_cast<uint16_t>(baseIndex + i + 1);
								++indices_index;
							}
							else
							{
								reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>(centerIndex);
								++indices_index;
								reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>(baseIndex + i);
								++indices_index;
								reinterpret_cast<uint32_t*>(indices)[indices_index] = static_cast<uint32_t>(baseIndex + i + 1);
								++indices_index;
							}
						}
					}
					for (size_t i = 0; i < m_meshData->numberOfIndices; i++)
					{
#if defined( EAE6320_PLATFORM_D3D )
						if (typeOfIndexData == 16)
						{
							reinterpret_cast<uint16_t*>(m_meshData->indexData)[m_meshData->numberOfIndices - 1 - i] = reinterpret_cast<uint16_t*>(indices)[i];
						}
						else
						{
							reinterpret_cast<uint32_t*>(m_meshData->indexData)[m_meshData->numberOfIndices - 1 - i] = reinterpret_cast<uint32_t*>(indices)[i];
						}
#elif defined( EAE6320_PLATFORM_GL )
						if (typeOfIndexData == 16)
						{
							reinterpret_cast<uint16_t*>(m_meshData->indexData)[i] = reinterpret_cast<uint16_t*>(indices)[i];
						}
						else
						{
							reinterpret_cast<uint32_t*>(m_meshData->indexData)[i] = reinterpret_cast<uint32_t*>(indices)[i];
						}
#endif
					}
					if (indices)
					{
						free(indices);
						indices = nullptr;
					}
				}
				if (!m_mesh->Initialize(*m_meshData))
				{
					EAE6320_ASSERT(false);
					Logging::OutputError("Failed to initialize debug line");
				}
			}
		}
	}
}
#endif