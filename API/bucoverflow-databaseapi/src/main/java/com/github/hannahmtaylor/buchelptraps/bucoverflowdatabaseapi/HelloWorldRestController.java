package com.github.hannahmtaylor.buchelptraps.bucoverflowdatabaseapi;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class HelloWorldRestController {

	public HelloWorldRestController() {
		// nothing to do
	}

	@GetMapping("/helloworld")
	String helloWorld() {
		return "Hello World!";
	}
	
	@GetMapping("/helloworld-json")
	HelloWorld helloWorld2() {
		return new HelloWorld("Hello world!");
	}
}
