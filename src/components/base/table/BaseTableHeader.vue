<template>
  <thead :class="$options.name">
    <th
      :class="`${$options.name}__cell`"
      v-for="column in columns"
      :key="column.name"
    >
      {{ column.text }}
    </th>
    <th key="edit" v-if="isEditable" :class="`${$options.name}__cell`"></th>
    <th key="delete" v-if="isRemovable" :class="`${$options.name}__cell`"></th>
  </thead>
</template>

<script>
export default {
  name: "BaseTableHeader",

  props: {
    columns: Array,
    isEditable: Boolean,
    isRemovable: Boolean
  }
};
</script>

<style lang="scss">
.BaseTableHeader {
  &__cell {
    padding: 0px;
    border-right: 0.05px solid gray;
    border-bottom: 0.05px solid gray;
  }

  &:last-of-type {
    border: 0px;
  }
}
</style>
