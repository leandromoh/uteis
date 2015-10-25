function formToJSON(form)
{
    var jsonForm = "";

    for(var i = 0; i < form.length; i++)
        jsonForm += ", " + form[i].name + " = "  + form[i].value;

    return "{" + jsonForm.substring(2) + "}";
}
