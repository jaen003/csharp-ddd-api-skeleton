check_directories := Src Tests

.PHONY: format
format:
	@echo "Running the project formatters"
	@dotnet csharpier $(check_directories)

.PHONY: test
test:
	@echo "Running the test suite of the project"
	@dotnet test --no-restore

.PHONY: lint
lint:
	@echo "Running the project linters"
	@dotnet csharpier --check $(check_directories)
	@dotnet roslynator analyze
