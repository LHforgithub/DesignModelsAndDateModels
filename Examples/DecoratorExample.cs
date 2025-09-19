using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDM.Model;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

namespace MDM.Examples
{
    internal abstract class DecoratableExample :
        IDecoratorManager<DecoratableExample, DecoratorExample>,
        IComponent<DecoratorExample>
    {
        public DecoratableExample OriginalDecoratableValue { get; set; }
        public DecoratableExample DecoratedValue => this;
        public List<DecoratorExample> Components { get; } = new();

        public void AddDecorator(DecoratorExample decorator) => AddComponent(decorator);
        public void RemoveDecorator(DecoratorExample decorator) => RemoveComponent(decorator);
        public void SetOriginalDecoratableValue(DecoratableExample obj)
        {
            OriginalDecoratableValue = obj;
        }
        public int AddComponent(DecoratorExample component)
        {
            var id = (int)(DateTime.Now.ToBinary() >> 32) + new Random().Next();
            component.SetComponentID(id);
            Components.Add(component);
            return id;
        }
        public bool RemoveComponent(DecoratorExample component)
        {
            return Components.Remove(component);
        }
        public bool TryRemoveComponent<U>(out U removed) where U : DecoratorExample
        {
            removed = default;
            if (Components.FirstOrDefault(x=>x is U) is U @out)
            {
                removed = @out;
                return true;
            }
            return false;
        }
        public bool TryGetComponent<U>(out U component) where U : DecoratorExample
        {
            throw new NotImplementedException();
        }
        public DecoratorExample GetComponent(string name)
        {
            throw new NotImplementedException();
        }
        public DecoratorExample GetComponent(int id)
        {
            throw new NotImplementedException();
        }
        public virtual string CallStringValue()
        {
            return "";
        }
        public virtual void CallVoidValue()
        {

        }

    }
    internal abstract class DecoratorExample :
        DecoratableExample,
        IDecorator<DecoratableExample, DecoratorExample>,
        IComponentComposite
    {
        public virtual DecoratableExample DecorateObject { get; set; }

        public virtual string ComponentName { get; set; }
        public int ComponentID { get; private set; }
        public void SetComponentID(int componentID) => ComponentID = componentID;
        public virtual void SetDecotateObject(DecoratableExample decorateObject) => DecorateObject = decorateObject;

        public override string CallStringValue() => "";
        public override void CallVoidValue() { }
    }
}
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
