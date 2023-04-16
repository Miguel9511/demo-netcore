(function () {

    document.addEventListener('DOMContentLoaded', async function () {
        CrearSelect('#cboTipoDocumento', TipoDocumento, 'value', 'value');
        CrearSelect('#cboSexo', Sexo, 'value', 'text');
        CrearSelect('#cboSexoFiltro', Sexo, 'value', 'text');
        lista();
        departamentos();
        $("#btnNuevo").click(() => {
            mostrarModal(Modelo_base);
        })
        $("#btnGuardar").click(() => {
            Transaccion();
        })
        $("#tbContacto tbody").on("click", ".btn-editar", function () {
            let contacto = $(this).data("modelo")

            mostrarModal(contacto)
        })
        $("#cboSexoFiltro").on("change", function () {
            let filtro = $(this).val();
            lista2(filtro);
        })
        $("#tbContacto tbody").on("click", ".btn-eliminar", function () {
            let idcontacto = $(this).data("id")
            Eliminar(idcontacto)
        })
        $("#cboDepartamento").on("change", function () {
            let idDepartamento = $(this).val();
            provincias(idDepartamento);
            $("#cboProvincia").val("");
            $("#cboDistrito").val("");
        })
        $("#cboProvincia").on("change", function () {
            let idProvincia = $(this).val();
            distritos(idProvincia);
            $("#cboDistrito").val("");
        })
    });

})()
const TipoDocumento = [
    { value: 'DNI' },
    { value: 'RUC' }
];
const Sexo = [
    { value: 'M', text:'MASCULINO' },
    { value: 'F', text:'FEMENINO' },
];

let idprovincia = '';
let iddistrito = '';
const Modelo_base = {
    id: 0,
    tipoDocumento: "",
    numeroDocumento: "",
    nombres: "",
    sexo: "",
    departamento: {
        id:""
    },
    provincia: {
        id: ""
    },
    distrito: {
        id:""
    }
}
function mostrarModal(modelo) {

    $("#txtId").val(modelo.id);
    $("#cboTipoDocumento").val(modelo.tipoDocumento)
    $("#txtNroDocumento").val(modelo.numeroDocumento)
    $("#txtNombres").val(modelo.nombres)
    $("#cboSexo").val(modelo.sexo)
    $("#cboDepartamento").val(modelo.departamento.id).change()
    $("#cboProvincia").change()
    idprovincia = modelo.provincia.id
    iddistrito = modelo.distrito.id
    //$("#cboDistrito").val(modelo.distrito.id)
    $('.modal').modal('show');
}


async function Transaccion() {
    try {
        let res;
        let NuevoModelo = JSON.parse(JSON.stringify(Modelo_base));
        NuevoModelo["id"] = $("#txtId").val();
        NuevoModelo["tipoDocumento"] = $("#cboTipoDocumento").val();
        NuevoModelo["numeroDocumento"] = $("#txtNroDocumento").val();
        NuevoModelo["nombres"] = $("#txtNombres").val();
        NuevoModelo["sexo"] = $("#cboSexo").val();
        NuevoModelo["departamento"]["id"] = $("#cboDepartamento").val();
        NuevoModelo["provincia"]["id"] = $("#cboProvincia").val();
        NuevoModelo["distrito"]["id"] = $("#cboDistrito").val();
        if ($("#txtId").val() == "0") {
            res = await mFetch("Home/Insertar", NuevoModelo, "POST")
            if (res.valor) {
                AlertSwal("Registrado con exito!", "Operacion");
                $('.modal').modal('hide');
                lista();
            } else {
                toastr.error("Ocurrio un Error", "Alerta");
            }
        } else {
            res = await mFetch("Home/Actualizar", NuevoModelo, "PUT")
            if (res.valor) {
                AlertSwal("Editado con exito!", "Operacion");
                $('.modal').modal('hide');
                lista();
            } else {
                toastr.error("Ocurrio un Error", "Alerta");
            }
        }
    } catch (e) {
        toastr.error(e.message.toString(), "Error");
    }
}

function lista() {

    fetch("Home/Lista")
        .then((response) => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then((dataJson) => {
            llenarTabla(dataJson)
        })

}

function lista2(filtro) {

    if (filtro) {
        fetch("Home/Lista2?filtro=" + filtro)
            .then((response) => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then((dataJson) => {
                llenarTabla(dataJson)
            })
    } else {
        lista()
    }

    

}
function llenarTabla(array) {
    $("#tbContacto tbody").html("");
    array.forEach((item) => {
        let color = item.sexo == 'M' ? "table-info" : "table-warning"
        $("#tbContacto tbody").append(
            $("<tr class=" + color + ">").append(
                $("<td>").text(item.tipoDocumento),
                $("<td>").text(item.numeroDocumento),
                $("<td>").text(item.nombres),
                $("<td>").text(item.sexo),
                $("<td>").text(item.departamento.descripcion),
                $("<td>").text(item.provincia.descripcion),
                $("<td>").text(item.distrito.descripcion),
                $("<td>").append(
                    $("<button>").addClass("btn btn-primary btn-sm me-2 btn-editar").data("modelo", item).text("Editar"),
                    $("<button>").addClass("btn btn-danger btn-sm btn-eliminar").data("id", item.id).text("Eliminar")
                )
            )
        )

    })
    toastr.success('Registros Cargados');
}
function departamentos() {
    fetch("Home/Departamentos")
        .then((response) => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then((dataJson) => {
            CrearSelect('#cboDepartamento', dataJson, 'id', 'descripcion')
            
        })

}
function provincias(id) {

    fetch("Home/Provincias?id=" + id)
        .then((response) => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then((dataJson) => {
            CrearSelect('#cboProvincia', dataJson, 'id', 'descripcion', 'Cargando')
            if (idprovincia > 0) {
                $("#cboProvincia").val(idprovincia).change()
            }
        })

}
function distritos(id) {

    fetch("Home/Distritos?id="+id)
        .then((response) => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then((dataJson) => {
            CrearSelect('#cboDistrito', dataJson, 'id', 'descripcion', 'Cargando')
            if (iddistrito > 0) {
                $("#cboDistrito").val(iddistrito).change()
            }
        })

}

function Eliminar(idcontacto) {

    AlertSwalConfirm("Desea Elimninar el registro?", "Eliminar","warning", function () {
        fetch("Home/Eliminar?id=" + idcontacto, {
            method: "DELETE"
        })
        .then((response) => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then((dataJson) => {

            if (dataJson.valor) {
                AlertSwal("Registro Eliminado", "Operacion");
                lista();
            }
        })
    });

}

