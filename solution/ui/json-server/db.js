const employee = require("./content/employee");
const review = require("./content/review");
const feedback = require("./content/feedback");

module.exports = function() {
  return {
    employee,
    review,
    feedback
  };
};
