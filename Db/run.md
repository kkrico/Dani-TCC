## Run Mysql Container
docker run -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root -v C:\Users\ramos\docker-data\mysql:/var/lib/mysql --name mysqlserver mysql

