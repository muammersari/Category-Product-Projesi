function CategoryGetList() {
    $.ajax({
        url: '/Home/CategoryGetlist',
        type: "Get",
        dataType: 'json',
        success: function (response) {
            console.log(response);
            if (response != false) {
                for (var i = 0; i < response.length; i++) {
                    var option = document.createElement("option");
                    option.value = response[i].categoryId;
                    option.innerText = response[i].categoryName.toString();
                    $("#categoryList").append(option);
                }
            }
        },
        error: function (err) {
        }
    });
}

function CategoyAdd() {
    if ($("#categoryName").val().trim() != "") {
        var category = {
            CategoryId: 0,
            CategoryName: $("#categoryName").val().trim()
        }
        $.ajax({
            url: '/Home/CategoryAdd',
            type: "Post",
            dataType: 'json',
            data: category,
            success: function (response) {
                if (response == false) {
                    alert("Kategori Eklenemedi");
                } else {
                    alert("Başarı ile eklendi");
                    console.log(response);
                    var option = document.createElement("option"); // eklenen seçeneği product alnındaki selecte yazdırdık.
                    option.value = response.categoryId;
                    option.innerText = response.categoryName.toString();
                    $("#categoryList").append(option);
                    $('#categoryArea input[type="text"]').each(function (i, e) { // categoryArea içindeki tüm input alanlarını temizledik
                        $(e).val("");
                    });
                }
            },
            error: function (err) {
            }
        });
    }
    else {
        alert("Lütfen Kategori İsmi Giriniz");
    }
}

function ProductAdd() {
    if ($("#productName").val().trim() != "" && $("#unitPrice").val().trim() != "" && $("#unitsInStock").val().trim() != "" && $("#unitPrice").val().trim()[0] != "," && $("#categoryList").val() != 0) {

        var prodcut = {
            ProdcutId: 0,
            ProductName: $("#productName").val().trim(),
            UnitsInStock: $("#unitsInStock").val().trim(),
            UnitPrice: $("#unitPrice").val().trim(),
            CategoryId: $("#categoryList").val()
        }
        $.ajax({
            url: '/Home/ProductAdd',
            type: "Post",
            dataType: 'json',
            data: prodcut,
            success: function (response) {
                if (response == false) {
                    alert("Ürün Eklenemedi");
                } else {
                    console.log(response);
                    //Burada eklenen ürün sayfa yenilenmeden tabloya yazdırılıyor
                    var trProduct = document.createElement("tr");
                    trProduct.id = "tr" + response.productId;
                    var tdbtndelete = document.createElement("td");
                    var btnDelete = document.createElement("input");
                    btnDelete.type = "button";
                    btnDelete.id = response.productId;
                    btnDelete.className = "btn btn-danger btn-sm btnProductDelete";
                    btnDelete.value = "Sil";
                    tdbtndelete.appendChild(btnDelete);
                    btnDelete.onclick = function () { // ürün silindiğinde silme işlemi yapılıyor
                        ProductDelete(this.id);
                    }

                    var tdProductId = document.createElement("td");
                    tdProductId.innerText = response.productId.toString();
                    var tdCategoryName = document.createElement("td");
                    tdCategoryName.innerText = response.category.categoryName;
                    var tdProductName = document.createElement("td");
                    tdProductName.innerText = response.productName;
                    var tdUnitPrice = document.createElement("td");
                    tdUnitPrice.innerText = response.unitPrice;
                    var tdUnitsInStock = document.createElement("td");
                    tdUnitsInStock.innerText = response.unitsInStock;
                    trProduct.append(tdbtndelete);
                    trProduct.append(tdProductId);
                    trProduct.append(tdCategoryName);
                    trProduct.append(tdProductName);
                    trProduct.append(tdUnitPrice);
                    trProduct.append(tdUnitsInStock);
                    document.getElementById("productBody").append(trProduct); //veriler tabloya ekleniyor

                    $('#productArea input[type="text"]').each(function (i, e) { // productArea içindeki tüm input alanlarını temizledik
                        $(e).val("");
                    });
                    alert("Başarı ile eklendi");
                }
            },
            error: function (err) {
            }
        });
    } else {
        alert("Lütfen Ürün Bilgilerini Eksiksiz Doldurunuz");
    }
}

function ProductDelete(id) { // tablodan ürün silme fonksiyonu
    $.ajax({
        url: '/Home/ProductDelete',
        type: "Post",
        data: { prodcutId: id },
        success: function (response) {
            if (response == false) {
                alert("Ürün Silinemedi");
            } else if(response == true) {
                alert("Başarı ile Sillindi");
                $("#tr" + id + "").hide(400, function () {
                    $("#tr" + id + "").remove();
                });
            }
        },
        error: function (err) {
        }
    });
}
function isNumberKey(evt) { //sadece sayı ve virgül girilmesini sağlayan fonksiyon
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode > 47 && charCode < 58) || charCode == 44)
        return true;
    return false;
}


function FirstValueCheck(id) { //ik değerin 0 yada virgül mü olduğu kontrol ediliyor. öyle ise ilk değer siliniyor
    if ($("#" + id + "").value[0] == "," || $("#" + id + "").value[0] == "0") {
        $("#" + id + "").value = $("#" + id + "").value.substring(1);
        alert("İlk değer ',' veya '0' olamaz");
    }
}

