package com.github.hannahmtaylor.buchelptraps.bucoverflowdatabaseapi;

import javax.sql.DataSource;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.jdbc.datasource.DriverManagerDataSource;

import com.fasterxml.jackson.databind.ObjectMapper;

@Configuration
public class Config {
	
	@Bean
	DataSource dataSource() {
		DriverManagerDataSource datasrc = new DriverManagerDataSource();
		datasrc.setDriverClassName("org.sqlite.JDBC");
		datasrc.setUrl("sqlite://./database.sqlite3");
		return datasrc;
	}
	
}
