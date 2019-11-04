using AutoFixture;
using AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace UnitTestsWeatherAPI
{
    public class FixtureFactory
    {
        private readonly Fixture _fixture;

        public FixtureFactory()
        {
            _fixture = new Fixture();

        }

        public FixtureFactory WithDefaults()
        {
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });

            _fixture.RepeatCount = 1;

            _fixture.Customize<ControllerContext>(c => c
                .Without(x => x.DisplayMode));

            return this;
        }

        public FixtureFactory WithRepeatCount(int repeatCount)
        {
            _fixture.RepeatCount = repeatCount;
            return this;
        }


        public Fixture Create()
        {
            return _fixture;
        }
    }
}
