SOURCE_DIRECTORY = Src
TEST_DIRECTORY = Tests

.PHONY: format
format:
	@echo "Running the project formatters"
	@dotnet csharpier $(SOURCE_DIRECTORY) $(TEST_DIRECTORY)

.PHONY: test
test:
	@echo "Running the test suite of the project"
	@dotnet test --no-restore --collect:"XPlat Code Coverage"
	@dotnet reportgenerator -filefilters:"-*/Infrastructure/**/*.cs;-*.g.cs" \
		-reports:"*/TestResults/**/*.xml" -targetdir:"coverage" -reporttypes:Html
	@rm -rf */TestResults

.PHONY: lint
lint:
	@echo "Running the project linters"
	@dotnet roslynator analyze
	@dotnet csharpier --check $(SOURCE_DIRECTORY) $(TEST_DIRECTORY)
