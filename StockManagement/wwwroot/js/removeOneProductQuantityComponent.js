class RemoveOneProductQuantityComponent extends HTMLElement {
    #action;

    constructor() {
        super();

        this.#action = null;
    }

    connectedCallback() {
        this.#action = this.dataset.action;

        if (!this.#action) {
            throw Error('[RemoveOneProductQuantityComponent] Action undefined');
        }

        this.render()
            .catch(e => console.error('An error appenned while decreasing the product quantity'));
    }

    async render() {
        this.outerHTML = `<button data-action="${this.#action}" class="btn btn-primary rounded decrease-product-qty">-</button>`;

        document.addEventListener('click', async (event) => {
            const target = event.target;

            if (target.classList.contains('decrease-product-qty')) {
                const action = target.dataset.action;

                const response = await fetch(action, {
                    method: 'PUT',
                });

                const validErrorCodes = [200, 302];

                // status !ok
                if (!validErrorCodes.includes(response.status)) {
                    const message = await response.text();
                    alert(`Une erreur est survenue durant la réduction de la quantité du produit : ${message}`);
                }

                // we reload the page, not the best practice, but it works
                document.location.reload();
            }
        });
    }
}

export default RemoveOneProductQuantityComponent;
