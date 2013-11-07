
function latestExhibits() {

    var data = Everlive.$.data('Exhibit');
    var query = new Everlive.Query();
    query.orderDesc('CreatedAt').take(4);
    data.get(query)
        .then(function (data) {
            alert(JSON.stringify(data));
        },
        function (err) {
            alert(JSON.stringify(err));
        });

}