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
      @on-edit="onEdit"
      @on-delete="onDelete"
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
  },

  methods: {
    onEdit(evt, employee) {
      this.$emit("on-edit", evt, employee);
    },

    onDelete(evt, employeeId) {
      this.$emit("on-delete", evt, employeeId);
    }
  }
};
</script>

<style lang="scss">
.EmployeeTable {
  border: 0.05px solid gray;
}
</style>
