function AppViewModel() {
    var self = this;
    self.firstName = ko.observable("Alexandre");
    self.lastName = ko.observable("Amorim");
    self.fullName = ko.computed(function () {
        return self.firstName() + " " + self.lastName();
    });
}

rootVM = new AppViewModel();
// Activates knockout.js
ko.applyBindings(rootVM);