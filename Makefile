deploy:
	@docker build -f src/Troas.Customer.Api/Dockerfile -t troasfl/troas-customer-api ./src
