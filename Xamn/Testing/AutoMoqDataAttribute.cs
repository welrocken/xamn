using System;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;

namespace Xamn.Testing
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }

        public AutoMoqDataAttribute(Type type, params object[] parameters)
            : this()
        {
            object obj = Activator.CreateInstance(type, parameters);

            base.Fixture.Customize(obj as ICustomization);
        }
    }
}