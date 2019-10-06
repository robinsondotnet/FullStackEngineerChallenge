export default {
  setEmployees(state, employees) {
    state.employees = employees;
  },
  setReviewFeedbackAssignee(state, { employeeId, feedbackAssigneeEmployees }) {
    state.employees = state.employees.map(employee => {
      if (employee.id === employeeId) {
        employee.feedbackAssigneeEmployees = feedbackAssigneeEmployees;
      }

      return employee;
    });
  }
};
