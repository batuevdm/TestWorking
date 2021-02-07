$().ready(function () {

    // Добавление нового контакта для человека
    $('#add-contact').click(function () {
        let contact = $('#people-contacts').last();
        contact.append(contact.find(".people-contact").last().clone());
        contact.find("input").last().val("");
    });

    // Выбор нужного поля в контактах
    let selects = $("select.form-control");
    for (i = 0; i < selects.length; i++) {
        let select = $(selects[i]);
        let index = select.data("selected");
        if (index) {
            select.find("option[value=" + index + "]").attr("selected", "selected");
        }
    }
})

function deleteContact() {
    return confirm("Удалить контакт?");
}