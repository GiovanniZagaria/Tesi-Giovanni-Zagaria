from flask import Flask, request, jsonify
from together import Together

app = Flask(__name__)

# Inizializza l'API Together
client = Together(api_key="29753fd69be8903061f2b955add0fae38638917867f0ebe08ef643d2a75372be")

@app.route('/get_suggestion', methods=['POST'])
def get_suggestion():
    # Estrai la storia dal corpo della richiesta
    data = request.json
    story_context = data.get('story', '')

    # Chiamata all'API Together AI
    response = client.chat.completions.create(
        model="meta-llama/Meta-Llama-3.1-8B-Instruct-Turbo",
        messages=[{"role": "user", "content": story_context}],
        max_tokens=512,
        temperature=0.7,
        top_p=0.7,
        top_k=50,
        repetition_penalty=1,
        stop=["<|eot_id|>", "<|eom_id|>"],
        stream=False
    )

    suggestion = response.choices[0].message.content
    return jsonify({"suggestion": suggestion})

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
