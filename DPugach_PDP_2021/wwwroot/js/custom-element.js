/**
 * Timer based on custom event approach
 */
class CustomEventTimer extends HTMLElement {

    // браузер вызывает этот метод при добавлении элемента в документ
    connectedCallback() {
        if (!this.rendered) {
            this.render();
            this.rendered = true;
            this.addEventListener('tick', event => {
                this.render(event.detail);
            });
        }
    }

    // браузер вызывает этот метод при удалении элемента из документа
    disconnectedCallback() {
        if (!this.rendered) {
            clearTimeout(this.timer);
        }
    }

    render(dateTime) {
        const options = {
            year: 'numeric', month: '2-digit', day: '2-digit',
            hour: '2-digit', minute: '2-digit', second: '2-digit',
            hour12: false
        };
        dateTime = dateTime || Date.now();
        this.innerHTML = new Intl.DateTimeFormat('ru-Ru', options).format(dateTime);
    }

    // у элемента могут быть ещё другие методы и свойства
}

/**
 * Timer based on attribute change approach.
 */
class ChangeAttributeTimer extends HTMLElement {

    // браузер вызывает этот метод при добавлении элемента в документ
    connectedCallback() {
        if (!this.rendered) {
            this.render(new Date());
            this.rendered = true;
        }
    }

    // браузер вызывает этот метод при удалении элемента из документа
    disconnectedCallback() {
        if (!this.rendered) {
            clearTimeout(this.timer);
        }
    }

    // массив имён атрибутов для отслеживания их изменений
    static get observedAttributes() {
        return ["date-time"];
    }

    // вызывается при изменении одного из перечисленных выше атрибутов
    attributeChangedCallback(name, oldValue, newValue) {
        this.render(newValue);
    }

    render(stringDateTime) {
        const options = {
            year: 'numeric', month: '2-digit', day: '2-digit',
            hour: '2-digit', minute: '2-digit', second: '2-digit',
            hour12: false
        };
        let dateTime = new Date(stringDateTime);
        this.innerHTML = new Intl.DateTimeFormat('ru-Ru', options).format(dateTime);
    }

    // у элемента могут быть ещё другие методы и свойства
}

customElements.define("custom-event-timer", CustomEventTimer);
customElements.define("change-attribute-timer", ChangeAttributeTimer);