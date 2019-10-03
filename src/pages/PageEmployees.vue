<template>
  <div :class="$options.name">
    <div :class="`${$options.name}__title`">
      <h1>{{ title }}</h1>
    </div>
    <employee-table :employees="employees" />
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
      title: "Employees"
    };
  },

  created() {
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
