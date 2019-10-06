<template>
  <tbody :class="$options.name">
    <tr v-for="row in rows" :key="row.id">
      <base-table-body-cell
        v-for="(column, columnIndex) in columns"
        :key="`${row.id}-${columnIndex}`"
        :type="column.type"
        :custom-component="column.component"
        v-model="row[column.name]"
        :edit-mode="onEditionRow === row.id"
      />

      <td v-if="isEditable" :class="`${$options.name}__edit`">
        <span>
          <button type="button" @click="onEdit($event, row)">
            <i :class="`fa fa-${getSaveIcon(row.id)}`"></i>
          </button>
        </span>
      </td>

      <td v-if="isRemovable" :class="`${$options.name}__delete`">
        <span>
          <button type="button" @click="onDelete($event, row.id)">
            <i class="fa fa-trash"></i>
          </button>
        </span>
      </td>
    </tr>
  </tbody>
</template>

<script>
import BaseTableBodyCell from "./BaseTableBodyCell.vue";

export default {
  name: "BaseTableBody",

  components: {
    BaseTableBodyCell
  },

  props: {
    columns: Array,
    rows: Array,
    isEditable: Boolean,
    isRemovable: Boolean
  },

  data() {
    return {
      onEditionRow: 0
    };
  },

  methods: {
    onDelete(evt, rowKey) {
      this.$emit("on-delete", evt, rowKey);
    },

    onEdit(evt, row) {
      if (this.onEditionRow === row.id) {
        this.$emit("on-edit", evt, row.id);
        this.onEditionRow = 0;
        return;
      }

      this.onEditionRow = row.id;
    },

    getSaveIcon(rowKey) {
      if (this.onEditionRow === rowKey) {
        return "save";
      }

      return "edit";
    }
  }
};
</script>

<style lang="scss">
.BaseTableBody {
  &__edit {
    border-right: 0.05px solid gray;
  }

  &__delete {
    border-right: 0.05px solid gray;
  }
}
</style>
