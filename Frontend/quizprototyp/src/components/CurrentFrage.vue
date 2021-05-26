<template>
  <div v-if="guid != ''">
      <h2>Aktuelle Frage</h2>
      <p>{{ currentFrage }}</p>
      <img :src="qrCodeUrl"><br><br>
      <button v-on:click="LoadNextFrage">Nächste Frage</button>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import axios from 'axios';

@Component
export default class CurrentFrage extends Vue {
  @Prop() private guid: string = '';

  private currentFrage: string = '';
  private qrValue: string = 'TEstQRCode';

  private async LoadNextFrage() {
      const response = await axios.get('http://localhost:8888/api/frage?guid=' + this.guid);
      this.currentFrage = response.data.frageText;
      this.qrValue = response.data.id;
  }

  private async mounted() {
    this.LoadNextFrage();
  }

  private get qrCodeUrl(): string {
      return `https://api.qrserver.com/v1/create-qr-code/?data=${this.qrValue}&size=150x150`;
  }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
