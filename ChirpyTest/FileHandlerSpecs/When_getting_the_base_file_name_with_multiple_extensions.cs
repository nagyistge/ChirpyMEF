namespace ChirpyTest.FileHandlerSpecs
{
	using Chirpy.Exports;
	using Machine.Specifications;

	[Subject(typeof(FileHandler))]
	public class When_getting_the_base_file_name_with_multiple_extensions
	{
		static FileHandler fileHandler;
		static string filename;
		static string result;

		Establish context = () =>
			{
				fileHandler = new FileHandler();

				filename = @"c:\path\to\file.tmp.txt";
			};

		Because of = () => { result = fileHandler.GetBaseFileName(filename); };

		It should_be_correct_path = () => result.ShouldBeEqualIgnoringCase(@"c:\path\to\file.tmp");
	}
}