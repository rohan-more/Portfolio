#ifndef Vector3_H
#define Vector3_H

#include <iostream>
#include<math.h>
#include<assert.h>
using namespace std;
namespace Cohort6
{

	class Vector3
	{
		
	public :
		Vector3();
		Vector3(float i_x, float i_y, float i_z);

		inline float getX(void) const;
		inline float getY(void) const;
		inline float getZ(void) const;

		void setX(float i_x);
		void setY(float i_y);
		void setZ(float i_z);
		static const Vector3 Zero;
		Vector3 & operator+=(const Vector3 &lhs);
		void display();
		bool static compare(float, float);
		bool static isNan(float);
		
		

	private :
		float m_x, m_y, m_z;
		
	};


	Vector3 operator+(const Vector3 &lhs, const Vector3 &rhs);
	Vector3 operator-(const Vector3 &lhs, const Vector3 &rhs);
	Vector3 operator*(const Vector3 &lhs, const Vector3 &rhs);
	Vector3 operator/(const Vector3 &lhs, const Vector3 &rhs);
	bool operator==(const Vector3 &lhs, const Vector3 &rhs);
	
	

	

} //namespace Cohort6

#include "Vector3Inline.h"
#endif //