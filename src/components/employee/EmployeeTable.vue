<template>
  <div :class="$options.name">
    <employee-table-row
      v-for="employee in employees"
      :key="employee.id"
      :employee="employee"
    />
  </div>
</template>

<script>
import EmployeeTableRow from "./EmployeeTableRow.vue";

export default {
  name: "EmployeeTable",

  components: {
    EmployeeTableRow
  },

  props: {
    employees: Array
  }
};
</script>

<style lang="scss">
.EmployeeTable {
  display: grid;
  grid-column-gap: 1%;
  grid-row-gap: 1%;
  grid-template-columns: 1fr 1fr 1fr 1fr;
  grid-template-rows: auto;
  grid-template-areas: "item item item item";
}
</style>
