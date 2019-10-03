<template>
  <base-card :image-url="empployee.photo">
    <template #header>
      <h3>{{ employee.firstName }}</h3>
    </template>
    <template #body>
      <p :class="`${$options.name}__bodyText`">{{ employee.age }}</p>
    </template>
    <template #footer>
      <button>See details</button>
    </template>
  </base-card>
</template>

<script>
import BaseCard from "../base/BaseCard.vue";

export default {
  name: "EmployeeTableRow",

  components: {
    BaseCard
  },

  props: {
    employee: Object
  }
};
</script>

<style lang="scss">
.EmployeeTableRow {
  &bodyText {
    text-align: left;
    padding-left: 5%;
    padding-right: 5%;
  }
}
</style>
