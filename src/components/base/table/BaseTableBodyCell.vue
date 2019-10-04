<template>
  <td :class="[$options.name, actionClass]">
    <span v-if="type === 'number'">
      {{ cell.value || 0 }}
    </span>
    <span v-else-if="type === 'date'">
      {{ cell.value || "" }}
    </span>
    <span v-else>
      <base-text-box :edit-mode="editMode" :text="cell.value"></base-text-box>
    </span>
  </td>
</template>

<script>
import BaseTextBox from "@/components/base/input/BaseTextBox.vue";

export default {
  name: "BaseTableBodyCell",

  components: {
    BaseTextBox
  },

  props: {
    cell: Object,
    type: {
      type: String,
      default() {
        return "string";
      }
    },
    editMode: {
      type: Boolean,
      default() {
        return false;
      }
    }
  },

  computed: {
    isAction() {
      return this.type === "action";
    },
    iconClass() {
      return this.isAction ? `fa fa-${this.cell.value}` : "";
    },
    actionClass() {
      return this.isAction ? `${this.$options.name}--action` : "";
    }
  }
};
</script>

<style lang="scss">
.BaseTableBodyCell {
  padding: 0px;
  border-right: 0.05px solid gray;
  width: 200px;

  &--action {
    width: 50px;
  }
}
</style>
