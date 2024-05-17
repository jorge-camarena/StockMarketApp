db-start:
	docker run --name stock-app-database -e POSTGRES_PASSWORD=mysecretpassword -e POSTGRES_USER=root -e POSTGRES_DB=stock_app_db -p 5432:5432 -d postgres
db-migrate-up:
	cd StockManager.API; dotnet ef database update; cd ..
	
server-start:
	cd StockManager.API; dotnet run; cd ..
dev-server-start:
	cd StockManager.API; dotnet watch; cd ..
clear:
	docker container rm -f $$(docker container ls -aq) && \
	docker image rm -f $$(docker image ls -aq)

