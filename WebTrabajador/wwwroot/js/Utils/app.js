const mFetch = async (url, body, method) => {

    try {
        const response = await fetch(url, { method: method, body: JSON.stringify(body), headers: {'Content-Type': 'application/json;charset=utf-8'}, });
        return await response.json();
    } catch (e) {
        console.log(e)
        toastr.error(e.message.toString(), "ERROR");
        //invocar alerta
    }
}

function CrearSelect(obj, array, val, text, etiqueta, index) {
    try {
        $(obj).find('option').remove();
        if (index === undefined || index == false) {
            if (etiqueta === undefined)
                $(obj).append('<option value="" selected="selected"> -- Seleccione --</option>');
            else
                $(obj).append('<option value="" selected="selected"> -- ' + etiqueta + ' --</option>');
        }

        $.each(array, function (i, item) {
            var objtemp = item;
            $(obj).append('<option value="' + item[val] + '">' + item[text] + '</option>');
        });
    } catch (e) {
        alert(e.message.toString());
    }

};

function AlertSwalConfirm(text, Basetitle,type, fn_Confirm) {
    swal({
        title: Basetitle,
        text: text,
        type: type,
        //imageUrl: "../Images/ajax-loader.gif",
        showCancelButton: true,
        confirmButtonColor: '#1AC943',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        cancelButtonText: "No",
        closeOnConfirm: false
    }, function () {
        fn_Confirm();
    }
    );
};

function AlertSwal(text, title) {
    swal({
        title: title,
        text: text,
        type: 'success',
        ButtonColor: "1AC943",
    });
}