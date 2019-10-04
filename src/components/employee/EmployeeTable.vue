<template>
  <table :class="$options.name">
    <base-table-header
      :columns="columns"
      :is-editable="true"
      :is-removable="true"
    />
    <base-table-body
      :columns="columns"
      :rows="employees"
      :is-editable="true"
      :is-removable="true"
    />
  </table>
</template>

<script>
import BaseTableHeader from "@/components/base/table/BaseTableHeader.vue";
import BaseTableBody from "@/components/base/table/BaseTableBody.vue";

export default {
  name: "EmployeeTable",

  components: {
    BaseTableHeader,
    BaseTableBody
  },

  props: {
    columns: Array,
    employees: Array
  }
};
</script>

<style lang="scss">
.EmployeeTable {
  border: 0.05px solid gray;
}
</style>
