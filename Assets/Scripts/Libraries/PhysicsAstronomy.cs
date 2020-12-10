namespace PhysicsAstronomy
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    public class PhysicsFormuls : MonoBehaviour
    {
        public static double gravitationalConstant = 6.67 * Math.Pow(10,-11); //Гравитационная постоянная 6.6743 * 10^-11
        public static double GetSurfaceGravity(float mass, float radius) //Расчет постоянной гравитации
        {
            return Math.Round(((gravitationalConstant * (mass * Math.Pow(10, 21)))/(Math.Pow(radius,2)))/Math.Pow(10,9), 2); //Формула GM/R^2
        }
        public static double GetAngularVelocity(float turnaroundTime) //Угловая скорость вращения
        {
            return (2*3.14)/turnaroundTime; //Формула w = 2pi/T | 2pi = 360°(полный оборот вокруг солнца) | T - время оборота в секундах 
        }
        public static double GetLinerRotationSpeed(float turnaroundTime, float radius) //Линейная скрость вращения
        {
            return (GetAngularVelocity(turnaroundTime) * radius);
        }
        public static double GetTotalVelocity(float distanceToTheSun_OR_OrbitRadius, float turnaroundTime) //Общая скорость вращения 
        {
            return Math.Round((((distanceToTheSun_OR_OrbitRadius * Math.Pow(10,6)) * (2 * 3.14))/(turnaroundTime * 24 * 3600)),1); //Формула (a.e.* 2pi)/T | a.e. - Астрономическая единица (км) | 2pi = 360°(полный оборот вокруг солнца) | T - время оборота в секундах 
        }
        public static double GetFirstSpaceSpeed(float mass, float radius) //Первая космическая скорость
        {
            return Math.Sqrt(gravitationalConstant * ((mass/1000)/(radius/1000)))*Math.Pow(10,9); //Формула V = sqrt(g*R*10^6)
        }
        public static double GetSecondSpaceSpeed(float accelerationOfGravity, float radius) //Вторая космическая скорость
        {
            return Math.Round(Math.Sqrt(2 * accelerationOfGravity * (radius/1000)),2); //Формула V = sqrt(2*g*R)
        }
        public static double GetSphereVolume(float radius, int power) //Определение размра планеты (Для UNITY)
        {
            return ((4 / 3) * 3.14f * Convert.ToSingle(Math.Pow(radius,3))) / Convert.ToSingle(Math.Pow(10,power));
        }
        public static double GetRadius(float sphereVolume, int power) //Определение Радиуса планеты (Для UNITY)
        {
            return (Convert.ToSingle(Math.Pow(sphereVolume,(1 / 3))) / ((4 / 3) * 3.14f)) * Convert.ToSingle(Math.Pow(10,power));
        }
    }
}
