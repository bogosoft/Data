using Bogosoft.Testing.Objects;

namespace Bogosoft.Data.Tests
{
    static class Extractor
    {
        internal static class Generic
        {
            internal static object DistanceToPrimary(CelestialBody cb) => cb.Orbit.DistanceToPrimary;

            internal static object Mass(CelestialBody cb) => cb.Mass;

            internal static object Name(CelestialBody cb) => cb.Name;

            internal static object Type(CelestialBody cb) => cb.Type.ToString();
        }

        internal static class Typed
        {
            internal static float DistanceToPrimary(CelestialBody cb) => cb.Orbit.DistanceToPrimary;

            internal static float Mass(CelestialBody cb) => cb.Mass;

            internal static string Name(CelestialBody cb) => cb.Name;

            internal static string Type(CelestialBody cb) => cb.Type.ToString();
        }
    }
}