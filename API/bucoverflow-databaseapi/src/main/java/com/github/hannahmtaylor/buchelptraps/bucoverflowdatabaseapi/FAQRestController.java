package com.github.hannahmtaylor.buchelptraps.bucoverflowdatabaseapi;

import java.nio.charset.StandardCharsets;

import org.springframework.core.io.ClassPathResource;
import org.springframework.core.io.Resource;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

import com.fasterxml.jackson.databind.ObjectMapper;

@RestController
public class FAQRestController {
	private final ObjectMapper mapper = new ObjectMapper();
	// don't reorder the above below list
	private final FAQ[] list = generateFAQList();
	
	private FAQ[] generateFAQList() {
		try {
			Resource res = new ClassPathResource("faqs.txt");
			String input = res.getContentAsString(StandardCharsets.UTF_8);
			return mapper.readValue(input, FAQ[].class);
		} catch (Exception ex) {
			throw new AssertionError("FAQs did not load",ex);
		}
	}
	
	@GetMapping("/faqs")
	public FAQ[] getFaqs() {
		return list;
	}
}
