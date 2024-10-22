import os
from flask import Flask, request, jsonify
from together import Together

# Imposta direttamente l'API key nel client
client = Together(api_key="29753fd69be8903061f2b955add0fae38638917867f0ebe08ef643d2a75372be")

from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/get_suggestion', methods=['POST'])
def get_suggestion():
    data = request.json
    story = data.get('story', '')

    response = client.chat.completions.create(
        model="meta-llama/Meta-Llama-3.1-8B-Instruct-Turbo",
        messages=[{"role": "user", "content": story}],
        max_tokens=512,
        temperature=0.7,
        top_p=0.7,
        top_k=50,
        repetition_penalty=1,
        stop=["<|eot_id|>", "<|eom_id|>"],
        stream=True
    )

    suggestion = ""
    for token in response:
        if hasattr(token, 'choices'):
            suggestion += token.choices[0].delta.content

    return jsonify({"suggestion": suggestion})

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
