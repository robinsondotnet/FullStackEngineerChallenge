<template>
  <ul :class="$options.name">
    <li :class="`${$options.name}__titleWrapper`">
      <h3>{{ title }}</h3>
    </li>
    <li
      :class="`${$options.name}__optionsWrapper`"
      :key="menuOption.id"
      v-for="menuOption in menuOptions"
    >
      <app-icon-label
        :icon="menuOption.icon"
        :text="menuOption.text"
        :link="menuOption.link"
      />
    </li>
  </ul>
</template>

<script>
import AppIconLabel from "@/components/base/BaseIconLabel.vue";

export default {
  name: "AppSidebarMenu",

  components: {
    AppIconLabel
  },

  props: {
    menuOptions: Array,
    title: String
  }
};
</script>

<style lang="scss">
@import "@/scss/abstracts/_mixins.scss";
@import "@/scss/abstracts/_colors.scss";

.AppSidebarMenu {
  padding-left: 0px;
  margin-top: 0px;
  margin-bottom: 0px;

  &__titleWrapper {
    //padding-left: 0em;
    margin: 0px;
    list-style: none;
    -moz-box-shadow: inset 0 0 10px #000000;
    -webkit-box-shadow: inset 0 0 10px #000000;
    box-shadow: inset 0 0 10px #000000;
    padding: 0.5em 0 0.5em 0;
  }

  &__optionsWrapper {
    @include bg-transparent($secondary-color);
    list-style: none;
  }
}
</style>
