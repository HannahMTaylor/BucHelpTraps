FROM eclipse-temurin:19 AS build-env
# install maven
RUN apt-get update
RUN apt-get install -y maven
# copy in source and build it
WORKDIR /build
COPY ./bucoverflow-databaseapi/ .
RUN mvn package
RUN rm target/original-*.jar
RUN mv $(ls target/*.jar) target/bucoverflow-databaseapi.jar
# build done
# create runtime env
FROM eclipse-temurin:19 AS runtime-env
WORKDIR /cat-runtime
COPY --from=build-env /build/target/bucoverflow-databaseapi.jar bucoverflow-databaseapi.jar
EXPOSE 80 443
ENTRYPOINT ["java","-jar","bucoverflow-databaseapi.jar"]
