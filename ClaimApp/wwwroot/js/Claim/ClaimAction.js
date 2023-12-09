var base_url = "https://localhost:7247/api/";
$(document).ready(function () {
    GelAllPendingClaims();

    if ($("#hdnRole").val() == "Account") {
        $("#btnReject").hide()
    }
})

function GelAllPendingClaims() {
    var data = {
        "userid": localStorage.getItem("UserRid"),
        "role":$("#hdnRole").val()
    }
    $.ajax({
        "url": base_url + "Claim/GetPendingClaims",
        "method": "get",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
         "data": data,
        "success": function (response) {
            if (response.ok) {
                $("#tblPendingRequest").DataTable({
                    data: response.data,
                    columns: [
                        { "data": "claim_Title" },
                        { "data": "claim_Reason" },
                        { "data": "claimDt" },
                        { "data": "amount" },
                        { "data": "expenseDt" },
                        { "data": "nm" },
                        { "data": "claim_Description" },
                        {
                            "data": "evidence",
                            render: function (evidence) {
                                var btn = '<a class="btn btn-sm btn-info" onclick=DownloadFile("' + evidence + '")>View</a>'
                                return btn
                            }
                        },
                        {
                            "data": "id",
                            render: function (id) {
                                var btn = '<a class="btn btn-sm btn-info" onclick="ActionRequest(' + id + ')">Action</a>'
                                return btn
                            }
                        }
                        
                    ]
                })
            }
            else {
                $("#msg").html(response.message).css("color", "red")
            }
        },
        "error": function (err) {
            console.log(err)
        }
    })
}


function ActionRequest(claimid) {
    $("#modalAction").modal("show");
    $("#hdnClaimId").val(claimid)
}

$("#btnApprove").click(function () {
    if (confirm("Are you sure you want to approve this request?")) {
        ApproveRejectRequest(1);
    }
})

$("#btnReject").click(function () {
    if (confirm("Are you sure you want to reject this request?")) {
        ApproveRejectRequest(0);
    }
})

function ApproveRejectRequest(action) {
    var claim = {
        "role":$("#hdnRole").val(),
        "action":action,
        "remark": $("#txtRemerks").val(),
        "claimid": $("#hdnClaimId").val(),
        "userid": localStorage.getItem("UserRid")
    }
    $.ajax({
        "url": base_url + "Claim/ActionClaim",
        "method": "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        "data": JSON.stringify(claim),
        "success": function (response) {
            if (response.ok) {
                $("#modalAction").modal("hide")
                toastr.success(response.message)
                setTimeout(function () {
                    window.location.reload()
                }, 5000)
            }
            else {
                toastr.error(response.message)
            }
        },
        "error": function (err) {
            console.log(err)
        }

    })
}
