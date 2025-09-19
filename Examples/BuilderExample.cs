using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDM.Model;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
namespace MDM.Examples
{
    internal sealed class BuilderExample : IBuilder<BuildProduct, BuildFuncExample>
    {
        public IPreBuilder<BuildProduct> PreBuilder { get; } = new PreBuildExample();
        private HashSet<BuildFuncExample> BuildingFuncs { get; } = new HashSet<BuildFuncExample>();
        HashSet<BuildFuncExample> IBuilder<BuildProduct, BuildFuncExample>.BuildingFuncs => BuildingFuncs;

        public BuildProduct AssemblyLine(BuildProduct originalValue)
        {
            foreach(var func in OrderedBuildFuncs())
            {
                func.Build(originalValue);
            }
            return originalValue;
        }

        public BuildProduct Build() => AssemblyLine(PreBuilder.PreBuild());

        private BuildFuncExample[] OrderedBuildFuncs()
        {
            var result = new List<BuildFuncExample>(BuildingFuncs);
            result.Sort();
            return result.ToArray();
        }

        BuildFuncExample[] IBuilder<BuildProduct, BuildFuncExample>.OrderedBuildFuncs() => OrderedBuildFuncs();

        void IBuilder<BuildProduct, BuildFuncExample>.AddBuildFunc(BuildFuncExample buildFunc) => BuildingFuncs.Add(buildFunc);
        void IBuilder<BuildProduct, BuildFuncExample>.RemoveBuildFunc(BuildFuncExample buildFunc) => BuildingFuncs.Remove(buildFunc);
    }
    internal sealed class BuildProduct
    {
        public string Value { get; set; } = "";
    }
    internal sealed class PreBuildExample : IPreBuilder<BuildProduct>
    {
        public BuildProduct PreBuild() => new();
    }
    internal abstract class BuildFuncExample : IBuildingFunction<BuildProduct>
    {
        public int Priority { get; private set; } = 0;
        public abstract BuildProduct Build(BuildProduct originalValue);

        public virtual int CompareTo(IBuildingFunction<BuildProduct> other)
        {
            return Priority.CompareTo(other.Priority);
        }
        public void SetDecoratorPriority(int priority)  => Priority = priority;
    }
    internal sealed class BuildFunc_1 : BuildFuncExample
    {
        public override BuildProduct Build(BuildProduct originalValue)
        {
            originalValue.Value += "Decorate by BuildFunc_1\r\n";
            return originalValue;
        }
    }
    internal sealed class BuildFunc_2 : BuildFuncExample
    {
        public override BuildProduct Build(BuildProduct originalValue)
        {
            originalValue.Value += "Decorate by BuildFunc_2\r\n";
            return originalValue;
        }
    }
}
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
