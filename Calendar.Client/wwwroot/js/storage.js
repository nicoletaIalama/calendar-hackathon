// Simple localStorage wrapper for Blazor JS interop.
// Keep this API stable; C# code calls these functions by name.
window.enableCalendar = window.enableCalendar || {};
window.enableCalendar.storage = {
  get: function (key) {
    return window.localStorage.getItem(key);
  },
  set: function (key, value) {
    window.localStorage.setItem(key, value);
  },
  remove: function (key) {
    window.localStorage.removeItem(key);
  }
};
