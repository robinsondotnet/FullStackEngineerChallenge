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
        { name: "age", text: "Age", type: "number" }
      ]
    };
  },

  methods: {
    onEditEmployee(evt, index) {
      console.log(evt);
    },

    onDeleteEmployee(evt, index) {
      console.log(evt);
    }
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
