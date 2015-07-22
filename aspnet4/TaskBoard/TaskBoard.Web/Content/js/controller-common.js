var gb = gb || {};

gb.href = function (rel, model) {
    return _.find(model.links, function (x) { return x.rel === rel; }).href;
};

gb.formDataRequest = function (method, url, data) {
    return {
        method: method,
        url: url,
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        transformRequest: function (obj) {
            var str = [];
            for (var p in obj)
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
            return str.join("&");
        },
        data: data
    }
};