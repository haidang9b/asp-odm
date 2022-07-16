$(document).ready(function () {

    $(document).on("click", ".btn-accept-provide", function (e) {
        var id = $(e.target).data('id')

        $('#id-request-accept').html(id)
        $('#id-accept').val(id)
        $('#message-request-accept').html("")
        $('#accept-request').modal('show');
    })

    $(document).on("click", ".btn-return-request", function (e) {
        var id = $(e.target).data('id');

        $('#id-accept-return').val(id);
        $('#id-request-return').html(id);
        $('#accept-return').modal('show')
    })

    $('#btn-accept-return-request').click(e => {
        var id = $('#id-accept-return').val();
        acceptReturnRequest(id);
    })

    

    $(document).on("click", ".btn-return-product", function (e) {
        var id = $(e.target).data('id');
        var username = $(e.target).data('username');
        var fullname = $(e.target).data('fullname');
        var idproduct = $(e.target).data('idproduct');
        var nameProduct = $(e.target).data('nameproduct');
        var time = $(e.target).data('time');

        document.getElementById("form-request-return").reset();
        $('#username-return').val(username)
        $('#fullname-return').val(fullname)
        $('#id-product-return').val(idproduct)
        $('#name-product-return').val(nameProduct)
        $('#time-receive-return').val(time)
        $('#id-borrow-return').val(id)

        $('#return-request').modal("show");


    })

    function displaySuccess(message) {
        $(`#message-success-notification`).html(message)
        $(`#success-notification`).fadeTo(2000, 1)
        setTimeout(() => {
            $(`#success-notification`).fadeTo(2000, 0)
        })
        $(`#success-notification`).css(`display`,`none`);
    }

    function displayWarning(message) {
        $(`#warning-notification`).html(message)
        $(`#warning-notification`).fadeTo(2000, 1)
        setTimeout(() => {
            $(`#warning-notification`).fadeTo(2000, 0)
        })
    }

    $('#btn-return-submit').click(e => {
        e.preventDefault();
        var id = $('#id-borrow-return').val()

        requestReturn(id);
    })

    $('#btn-accept-request').click(e => {
        var id = $('#id-accept').val();
        acceptRequest(id);
    })


    $(document).on("click", ".btn-cancel-provide", function (e) {
        var id = $(e.target).data('id')
        $('#id-request-cancel').html(id)
        $('#id-cancel').val(id)
        $('#message-request-cancel').html("")
        $('#cancel-request').modal('show');
    })

    $('#btn-cancel-request').click(e => {
        var id = $('#id-cancel').val();
        cancelRequest(id);
    })

    $(document).on("click", ".btn-provide", function (e) {

        var id = $(e.target).data('id')
        var name = $(e.target).data('name')
        $('#name-product-provide').val(name);
        $('#id-prdduct-provide').val(id);
        $(`#errorProvide`).html("")
        $('#provideDevice').show();
    })

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });


    $("#btn-provide-submit").click(e => {
        var username = $('#username-provide').val();
        var idProduct = $('#id-prdduct-provide').val();

        provideProduct(idProduct, username);
    })

    $('#btn-upload-avatar').click(() => {
        var file_upload = $(`#avatar-upload`).prop('files')[0];
        if (file_upload === undefined) {
            $(`#message-change-avatar`).html(`<div class="alert alert-danger" role="alert">
                    Vui lòng chọn ảnh cần thay đổi
                </div>`)
        }
        else {
            var file_type = file_upload.type

            if (file_type.includes('image/')) {
                var fd = new FormData();
                fd.append('file', file_upload);
                $.ajax({
                    url: '/Account/UpdateAvatar',
                    type: 'POST',
                    data: fd,
                    contentType: false,
                    processData: false,
                    async: true,
                    cache: false,
                    success: (response) => {
                        if (response.success == true) {
                            displaySuccess(response.message)
                            $('#avatar-upload').val('')
                            $('#updateAvatar').hide();
                        }
                        else {
                            $(`#message-change-avatar`).html(`<div class="alert alert-danger" role="alert">
                                ${response.message}
                            </div>`)
                        }
                    }
                })
            }
            else {
                $(`#message-change-avatar`).html(`<div class="alert alert-danger" role="alert">
                File này không hỗ trợ cho ảnh đại diện
                            </div>`)
            }
        }

    })
    $('#username-provide').keyup(async function () {
        var query = $('#username-provide').val()
        $('#detail-search').html("");
        $(".list-group").css("display", "block");
        if (query.length > 1) {
            await fetch("/Account/Filter?keyword=" + query)
                .then(response => response.json())
                .then(data => {
                    var listItem = ``
                    for (var i = 0; i < data.length; i++) {
                        listItem += `<li class='list-group-item suggest-user-list' value='${data[i].username}'> ${data[i].username} | ${data[i].fullName} |  ${data[i].email}</li>`
                    }
                    $(".list-group").html(listItem);
                });
        }
        else {
            $(".list-group").css("display", "none");
        }
    })

    $(document).on("click", ".suggest-user-list", function (e) {
        var focusInput = document.getElementById('username-provide');
        focusInput.value = e.target.getAttribute("value");
        $(".list-group").css("display", "none");
    })
    async function acceptReturnRequest(id) {
        data = { id }
        await fetch("/Request/ReturnClientAccept", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)

        }).then(Response => {
            Response.json().then(data => {
                if (data.success) {
                    $("#message-request-return").html("")
                    $("#accept-return").hide();
                    $(".modal-backdrop").remove();
                    $(`#${id}request`).remove();
                    displaySuccess(data.message)
                }
                else {
                    var tmp = `<div class="alert alert-danger" role="alert">
                              ${data.message}
                            </div>`
                    $("#message-request-return").html(tmp)
                }
            })
        })
    }

    async function requestReturn(id) {
        data = { id }
        await fetch("/Request/ReturnClient", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)

        }).then(Response => {
            Response.json().then(data => {
                if (data.success) {
                    $('#return-request').modal('hide');
                    $('#error-return-request').html("");
                    $(`#${id}request`).remove();
                    displaySuccess(data.message)
                }
                else {
                    var tmp =`<div class="alert alert-danger" role="alert">
                              ${data.message}
                            </div>`
                    $('#error-return-request').html(tmp)
                }
            })
        })
    }

    async function cancelRequest(id) {
        data = { id };
        await fetch("/Request/CancelClient", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        }).then(Response => {
            Response.json().then(data => {
                if (data.success) {
                    $('#id-request-cancel').html("");
                    $('#id-cancel').val('');
                    $('#message-request-cancel').html("");
                    $(`#${id}request`).remove();
                    $('#cancel-request').modal('hide');
                    displaySuccess(data.message)
                }
                else {
                    var tmp = `<div class="alert alert-danger" role="alert">
                          ${data.message}
                        </div>`
                    $('#message-request-cancel').html(tmp)
                }
            })
        })
    }

    async function acceptRequest(id) {
        var data = { id }
        await fetch("/Request/AcceptClient", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        }).then(Response => {
            Response.json().then(data => {
                if (data.success) {
                    $('#id-request-accept').html("");
                    $('#id-accept').val('');
                    $('#message-request-accept').html("");
                    $(`#${id}request`).remove();
                    $('#accept-request').modal('hide');
                    displaySuccess(data.message)
                }
                else {
                    var tmp = `<div class="alert alert-danger" role="alert">
                          ${data.message}
                        </div>`
                    $('#message-request-accept').html(tmp)
                }
            })
            
        })
    }

    async function provideProduct(idProduct, username) {
        var data = { idProduct, username }
        await fetch("/Product/Provide/", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        }).then(Response => {
            Response.json().then(data => {
                if (data.success) {
                    $('#name-product-provide').val('');
                    $('#id-prdduct-provide').val('');
                    $('#username-provide').val('');
                    $('#errorProvide').val('')
                    $('#provideDevice').hide();
                    displaySuccess(data.message)

                }
                else {
                    var tmp = `
                            <div class="alert alert-danger" role="alert">
                          ${data.message}
                        </div>
                    `
                    $('#errorProvide').html(tmp)
                }
            })
        })
    }
});