using System;
using System.Reflection;
using static System.Activator;
using static System.Console;

namespace LateBindingWithDynamic
{
    class Program
    {
        static void Main()
        {
            WriteLine("***** Adding with reflection & dynamic keyword *****\n");
            AddWithReflection();
            AddWithDynamic();
            ReadLine();
        }

        private static void AddWithReflection()
        {
            WriteLine("Adding with reflection: ");

            Assembly asm = Assembly.Load("MathLibrary");

            try
            {
                // Get metadata for the SimpleMath type.
                Type math = asm.GetType("MathLibrary.SimpleMath");

                // Create a SimpleMath on the fly.
                object obj = CreateInstance(math);

                // Get info for Add.
                MethodInfo mi = math.GetMethod("Add");

                // Invoke method (with parameters).
                object[] args = { 10, 70 };
                WriteLine($"Result is: {mi.Invoke(obj, args)}");
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }


        private static void AddWithDynamic()
        {
            WriteLine("Adding with dynamic: ");

            Assembly asm = Assembly.Load("MathLibrary");

            try
            {
                // Get metadata for the SimpleMath type.
                Type math = asm.GetType("MathLibrary.SimpleMath");

                // Create a SimpleMath on the fly.
                dynamic obj = CreateInstance(math);
                WriteLine("Result is: {0}", obj.Add(10, 70));
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                WriteLine(ex.Message);
            }
        }

    }
}