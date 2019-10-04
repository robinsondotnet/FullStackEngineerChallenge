<template>
  <tbody :class="$options.name">
    <tr v-for="row in rows" :key="row.id">
      <base-table-body-cell
        v-for="(column, columnIndex) in columns"
        :key="columnIndex"
        :type="column.type"
        :cell="{ value: row[column.name] }"
        :edit-mode="editMode"
      />
      <td v-if="isEditable" :class="`${$options.name}__edit`">
        <span>
          <button type="button" @click="onEdit($event, row.id)">
            <i class="fa fa-edit"></i>
          </button>
        </span>
      </td>
      <td v-if="isRemovable" :class="`${$options.name}__delete`">
        <span>
          <button type="button" @click="onRemove($event, row.id)">
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
    isRemovable: Boolean,
  },

  data() {
    return {
      editMode: false
    }
  },

  methods: {
    onRemove(evt, rowKey) {
      this.$emit("on-remove", evt, rowKey);
    },

    onEdit(evt, rowKey) {
      if (this.editMode) {
        this.$emit("on-edit", evt, rowKey);
      }

      this.editMode = !this.editMode;
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
