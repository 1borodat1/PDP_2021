/**
 * Timer based on custom event approach
 */
class CustomEventTimer extends HTMLElement {

    // ������� �������� ���� ����� ��� ���������� �������� � ��������
    connectedCallback() {
        if (!this.rendered) {
            this.render();
            this.rendered = true;
            this.addEventListener('tick', event => {
                this.render(event.detail);
            });
        }
    }

    // ������� �������� ���� ����� ��� �������� �������� �� ���������
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

    // � �������� ����� ���� ��� ������ ������ � ��������
}

/**
 * Timer based on attribute change approach.
 */
class ChangeAttributeTimer extends HTMLElement {

    // ������� �������� ���� ����� ��� ���������� �������� � ��������
    connectedCallback() {
        if (!this.rendered) {
            this.render(new Date());
            this.rendered = true;
        }
    }

    // ������� �������� ���� ����� ��� �������� �������� �� ���������
    disconnectedCallback() {
        if (!this.rendered) {
            clearTimeout(this.timer);
        }
    }

    // ������ ��� ��������� ��� ������������ �� ���������
    static get observedAttributes() {
        return ["date-time"];
    }

    // ���������� ��� ��������� ������ �� ������������� ���� ���������
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

    // � �������� ����� ���� ��� ������ ������ � ��������
}

customElements.define("custom-event-timer", CustomEventTimer);
customElements.define("change-attribute-timer", ChangeAttributeTimer);