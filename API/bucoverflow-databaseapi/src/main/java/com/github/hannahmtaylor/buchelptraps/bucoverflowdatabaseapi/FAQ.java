package com.github.hannahmtaylor.buchelptraps.bucoverflowdatabaseapi;

public class FAQ {
	private String question;
	private String answer;
	
	public static FAQ of(String question, String answer) {
		FAQ faq = new FAQ();
		faq.setQuestion(question);
		faq.setAnswer(answer);
		return faq;
	}
	
	public final String getQuestion() {
		return question;
	}
	
	public final void setQuestion(String question) {
		this.question = question;
	}
	
	public final String getAnswer() {
		return answer;
	}
	
	public final void setAnswer(String answer) {
		this.answer = answer;
	}
}
