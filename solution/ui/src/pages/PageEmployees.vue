<template>
  <div :class="$options.name">
    <div :class="`${$options.name}__title`">
      <h1>{{ title }}</h1>
    </div>
    <employee-table
      :columns="columns"
      :employees="employees"
      @on-edit="onEditEmployee"
      @on-delete="onDeleteEmployee"
    />
  </div>
</template>

<script>
import EmployeeTable from "@/components/employee/EmployeeTable.vue";
import { mapState } from "vuex";

export default {
  name: "PageEmployees",

  components: {
    EmployeeTable
  },

  computed: {
    ...mapState({
      employees: state => state.employee.employees
    })
  },

  data() {
    return {
      title: "Employees",
      columns: [
        { name: "firstName", text: "First Name" },
        { name: "lastName", text: "Last Name" },
        { name: "age", text: "Age", type: "number" },
        { name: "reviewScore", text: "Performance Review", type: "star" },
        {
          name: "feedbackAssigneeEmployees",
          text: "Feedback Assignee",
          type: "multi-select",
          component: "feedback-assignment-multi-select",
          icon: "box"
        }
      ]
    };
  },

  methods: {
    onEditEmployee(evt, employee) {
      this.$store.dispatch("employee/updateEmployee", employee);
    },

    onDeleteEmployee(evt, employeeId) {
      this.$store.dispatch("employee/deleteEmployee", employeeId);
    }
  },

  mounted() {
    this.$eventBus.$on(
      "ON_REVIEW_FEEDBACK_ASSIGNED",
      ({ employeeId, feedbackAssigneeEmployees }) => {
        this.$store.commit("employee/setReviewFeedbackAssignee", {
          employeeId: parseInt(employeeId),
          feedbackAssigneeEmployees
        });
      }
    );
    this.$store.dispatch("employee/loadEmployees");
  }
};
</script>

<style lang="scss">
.PageEmployees {
  &__title {
  }
}
</style>
