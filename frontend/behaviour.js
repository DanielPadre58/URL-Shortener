const domainInput = document.getElementById('link_field');
const outputsContainer = document.getElementById('outputs');
const resultField = document.getElementById('shortened_url_field');

async function CreateUrl() {
    const domain = domainInput.value;

    try {
        const res = await fetch("http://localhost:5134/api/urls", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ domain })
        });

        const data = await res.json();

        outputsContainer.style.display = "flex";
        resultField.textContent = "http://localhost:5134/urls/" + data.shortenedUrlId;

    } catch (error) {
        console.error("Erro ao criar URL:", error);
        resultField.value = "Erro ao encurtar URL.";
    }
}
