import os
from flask import Flask, request, jsonify
from together import Together
import logging

log_path = r"C:\Users\miche\OneDrive\Desktop\Giovanni\FlaskApp\flask_app.log"
logging.basicConfig(filename=log_path, level=logging.INFO, 
                    format='%(asctime)s - %(levelname)s - %(message)s')

client = Together(api_key="29753fd69be8903061f2b955add0fae38638917867f0ebe08ef643d2a75372be")
app = Flask(__name__)

@app.route('/get_suggestion', methods=['POST'])
def get_suggestion():
    data = request.json
    choice = data.get('choice', '')
    story = data.get('story', '')
    logging.info(f"Richiesta ricevuta: choice: {choice}, story: {story}")

    try:
        response = client.chat.completions.create(
            model="meta-llama/Meta-Llama-3.1-8B-Instruct-Turbo",
            messages=[{"role": "user", "content": f"{choice} {story}"}],
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
            if hasattr(token, 'choices') and token.choices:
                suggestion += token.choices[0].delta.content

        logging.info(f"Risposta inviata: {suggestion}")
        return jsonify({"suggestion": suggestion})

    except Exception as e:
        logging.error(f"Errore durante la generazione del suggerimento: {str(e)}")
        return jsonify({"error": "Errore nella generazione del suggerimento"}), 500

@app.errorhandler(500)
def server_error(e):
    logging.error(f"Errore del server: {e}")
    return jsonify(error=str(e)), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000, debug=True)
