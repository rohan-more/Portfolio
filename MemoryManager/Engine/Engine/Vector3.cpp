#include "Vector3.h"

namespace Cohort6
{

	Vector3::Vector3()
	{

	}

	Vector3::Vector3(float i_x, float i_y, float i_z )
	{
		this->m_x = i_x;
		this->m_y = i_y;
		this->m_z = i_z;

		assert(!Vector3::isNan(i_x));
		assert(!Vector3::isNan(i_y));
		assert(!Vector3::isNan(i_z));

	}

   /* float Vector3::getX(void)const
	{
		return m_x;
	}

	float Vector3::getY(void)const
	{
		return m_y;
	}

	float Vector3::getZ(void)const
	{
		return m_z;
	}*/


	void Vector3::setX(float x)

	{
		this->m_x = x;
	}
	void Vector3::setY(float y)

	{
		this->m_y = y;
	}

	void Vector3::setZ(float z)

	{
		this->m_z = z;
	}

	void Vector3::display()
	{
		cout << getX() << "," << getY() << endl;
	}


	Vector3 operator+(const Vector3 &lhs, const Vector3 &rhs)
	{
		return Vector3(
		lhs.getX() + rhs.getX(),
		lhs.getY() + rhs.getY(),
		lhs.getZ() + rhs.getZ() );

	}


	Vector3 operator-(const Vector3 &lhs, const Vector3 &rhs)
	{
		return Vector3(
			lhs.getX() - rhs.getX(),
			lhs.getY() - rhs.getY(),
			lhs.getZ() - rhs.getZ());

	}

	Vector3 operator*(const Vector3 &lhs, const Vector3 &rhs)
	{
		return Vector3(
			lhs.getX() * rhs.getX(),
			lhs.getY() * rhs.getY(),
			lhs.getZ() * rhs.getZ());

	}

	Vector3 operator/(const Vector3 &lhs, const Vector3 &rhs)
	{
		return Vector3(
			lhs.getX() / rhs.getX(),
			lhs.getY() / rhs.getY(),
			lhs.getZ() / rhs.getZ());

	}

	Vector3 & Vector3:: operator+=(const Vector3 &lhs)
	{
		
		 m_x += lhs.m_x;
		m_y += lhs.m_y;
		 m_z += lhs.m_z;
			return *this;

	}

	bool operator==(const Vector3 &lhs, const Vector3 &rhs)
	{

		return(Vector3::compare(lhs.getX(), rhs.getX())) && (Vector3::compare(lhs.getY(), rhs.getY())) && (Vector3::compare(lhs.getZ(), rhs.getZ()));
		

	}


	bool Vector3::compare(float A, float B)
	{
		float maxRelDiff = FLT_EPSILON;
		float diff = fabs(A - B);
		A = fabs(A);
		B = fabs(B);
		
		float largest = (B > A) ? B : A;

		if (diff <= largest * maxRelDiff)
			return true;
		return false;
	}

	bool Vector3::isNan(float var)
	{
		volatile float d = var;
		return d != d;
	}
	
}