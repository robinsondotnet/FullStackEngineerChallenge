import axios from "axios";

export default {
  // TODO: Handle exceptions
  loadEmployees() {
    return axios
      .get("http://localhost:3000/employee")
      .then(response => response.data)
      .then(employees => {
        this.commit("employee/setEmployees", employees);
      });
  }
};
