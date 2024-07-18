using System.Reflection;

namespace AnalyticsService.Application
{
    public static class AssemblyReference
    {
        public static Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
