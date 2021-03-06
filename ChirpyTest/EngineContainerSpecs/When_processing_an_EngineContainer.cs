﻿namespace ChirpyTest.EngineContainerSpecs
{
	using System.Collections.Generic;
	using Chirpy;
	using ChirpyInterface;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	[Subject(typeof(EngineContainer))]
	public class When_processing_an_EngineContainer : EngineContainer_context
	{
		static EngineContainer engineContainer;
		static Mock<IEngine> engineMock;
		static string contents;
		static string filename;
		static List<EngineResult> result;

		Establish context = () =>
			{
				engineMock = AddEngine("DemoEngine", "1.0", "abc", "def");

				engineMock
					.Setup(e => e.Process(Moq.It.IsAny<string>(), Moq.It.IsAny<string>()))
					.Returns(new List<EngineResult> {new EngineResult {Contents = "output"}});

				engineContainer = new EngineContainer(Engines);

				contents = "ghi";
				filename = "jkl";
			};

		Because of = () => { result = engineContainer.Process(contents, filename); };

		It should_call_Process_on_the_engine = () =>
			engineMock.Verify(e => e.Process(contents, filename), Times.Once());

		It should_return_the_output_of_the_engine = () => result[0].Contents.ShouldEqual("output");
	}
}