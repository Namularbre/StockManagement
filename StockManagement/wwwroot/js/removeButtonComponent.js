
class RemoveButtonComponent extends HTMLElement {
    #text;
    #action;

    constructor() {
        super();

        this.#text = "Supprimer";
        this.#action = null;
    }

    connectedCallback() {
        if (this.dataset.text) {
            this.#text = this.dataset.text;
        }

        this.#action = this.dataset.action;

        if (!this.#action) {
            throw Error('[RemoveButtonComponent] Action undefined');
        }

        this.render()
            .catch(e => console.error('Une erreur est survenue durant la suppresion'));
    }

    async render() {
        this.outerHTML = `<button data-action="${this.#action}" class="btn btn-danger btn-remove">${this.#text}</button>`;

        document.addEventListener('click', async (event) => {
            let target = event.target;

            if (target.classList.contains('btn-remove')) {
                const action = target.dataset.action;

                const response = await fetch(action, {
                    method: 'DELETE',
                });

                const validErrorCodes = [200, 302];

                // status !ok
                if (!validErrorCodes.includes(response.status)) {
                    const message = await response.text();
                    alert(`Une erreur est survenue durant la suppression de la ressource : ${message}`);
                }

                // we reload the page, not the best practice, but it works
                document.location.reload();
            }
        });
    }

    static defineEvents() {
        
    }
}

export default RemoveButtonComponent;
