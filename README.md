# Zoom Fatigueを軽減させることで、快適なオンラインコミュニケーションを実現するアバタ動作システム
Zoomが抱えるインターフェースデザインの問題を解決し、利用者にとって快適なオンラインコミュニケーションを実現する。

<img width="250" alt="image" src="https://github.com/Takumi1209/AvatarSystemProject/assets/87661689/77c39998-b404-4993-9f50-6ee3dd0e819f">

### 使用技術
- Unity 2022.3.1f1
- C#
- OpenCV for Unity
- Dlib FaceLandmark Detector
- VRM, UniVRM
- UnityCapture
- StandaloneFileBrowser

## 背景
COVID-19の流行により、Web会議サービス「Zoom」の導入が急増しました。それに伴い、Zoomの使用によって引き起こされる「Zoom Fatigue」が問題視されています。Bailenson氏は、現在のZoomインターフェースデザインの様々な側面が心理的負荷に影響を与える可能性が高いことを示しました【Bailenson, 2021】。この問題を解決し、Web会議参加者にとって快適なオンラインコミュニケーションを実現する必要があります。

Zoom Fatigueを調査した研究では、Zoomを2回以上利用したことのある成人613人を対象にアンケート調査を実施しました。調査結果によると、Zoom疲労の度合いは性別や人種によって異なることがわかりました。また、Zoom疲労を軽減する実用的なアプローチとして、Zoom上でアバターを活用することが一つの解決方法として示唆されました。

## Zoom上でアバタを利用するメリット[Ratan et al. 2022]
通常、Zoom上で自己ビデオを完全に非表示にすることが一つの解決策として考えられます。しかし、これには自己呈示に対する意識の欠如や意図しない放送（背景の空間情報）の問題が発生する可能性があります。また、顔の平滑化や修正を行うフィルタでは、自己注視を十分に下げられない場合もあります。そこで、アバタを利用することでユーザ全体の映像を遮蔽し、否定的な自己注視を減少させることができます。また、アバタは、ユーザの行動に対して肯定的な影響を与えるよう設計することが可能です。

## 新規性
具体的にどのようにアバタを設計し、実装するかや実際に実装した結果に関しては、未だに検証されていません。そこで本研究では、この課題に対して具体的な設計や実装を行うことで一つの解決方法の提示に挑戦します。

## アプローチ方法
「Zoom Fatigue」を軽減するためにチューニングされたアバタ動作システムを開発します。本システムをZoom上で利用することで、Web会議上での快適なコミュニケーション環境を実現します。
本研究で提案するシステムでは、Zoom Fatigueの問題点を解決するために必要な表情及び身体動作だけを反映、表示するシステムを実装します。

## アバタ動作システムを実装するための手順
Zoom Fatigueの原因となる4つの問題に対してそれぞれ解決方法を考えます。その解決方法から考えられるアバタ動作システムに実装すべき機能を決定します。また、人物の背景空間情報がZoom参加者に与える印象も考慮して実装を行います。

## Zoom Fatigueの４つの原因
### 1. 至近距離での視線に曝されること
Zoom上において他人と長時間直接視線を向けあったり、顔を近くで見たりすることが当たり前となってしまっています。
- 解決方法：自分の分身としてのアバタを間に置くことで直接的な視線に曝されることを防ぐ
- 実装機能：Zoom上で簡単にアバタを利用できるようにすること
  
### 2. 対面よりも動作や表情がわかりづらいため、非言語的コミュニケーションが伝わりづらいこと
Web会議参加者の非言語的行動を意識的に監視し、意図的に合図を大げさに相手に送ることが強いられています。
- 解決方法：アバタによって自分の動作および表情を強調表示する
- 実装機能：動作および表情の検知し、検知した動作および表情を自己アバタが強調して表現する
- 対面の場合、1つの非言語行動が特定の意味を持つことは第三者にも容易に理解できます。しかし、Zoom上では、発信者の意図とは異なる意味に受け取られることがあります。特に表情が乏しい人は、誤解を招きやすいです。このような誤解を避けるためには、非言語行動を明示的に表現でき、簡単に行える機能が必要です。
- 他者に対して親しみやすさや信頼感を伝え、コミュニケーションを円滑にするような表情(笑顔、疑問、感動・感嘆)を実装します。

### 3. 自分の顔を常に意識しなければならないこと
Zoom上では、自分の姿を常に見なければいけない状況にあるため、自己注視が強いられています。
- 解決方法：当人にとって好ましい見た目のアバタを利用できること
- 実装機能：システム上で多様なアバタを利用可能にすること

### 4. 画面内に収まり続けなければならないために、身体移動が制限されてしまうこと
カメラに映る自分の姿を中心に維持し、自分の顔を他の人が見るために十分な大きさに保つことが強いられます。
- 解決方法：一時的に画面外に移動しても、常にアバタが画面中心部に位置し、自然に振る舞う
- 実装機能：顔認識・追跡し、人がカメラ外に行った時に一時的にアバタが自動で振る舞ってくれるようにすること
- 対面では、歩いたり立ち上がったり、飲み物を取りに行くといった行動が自然にできます。しかし、Zoom上ではそのような行動が制限されてしまいます。これを解消するためには、対面で許されていたこれらの行動がZoomでも可能になるような仕組みが必要です。

<img width="492" alt="image" src="https://github.com/Takumi1209/AvatarSystemProject/assets/87661689/2d748ca1-ca9e-4ef3-bb7c-ee4dee4690c4">


## 人物の背景空間情報の考慮
Zoom上では、人物の背景空間情報が意図しないアウティングにつながってしまいます。また、ヴァーチャル背景を設定していたとしてもその背景が好ましい印象を与えるとは限りません。
- 解決方法：好ましい背景空間を表示する。先行研究より、人にとって好ましいと考えられている条件を網羅した背景空間を利用する[岡田 他. 2021] [石川 他. 2013] [Bailenson et al. 2023] 
- 実装機能：システム内で多様な背景空間の選択ができること
- 背景空間情報の条件
  - 場所:屋外
  - 天気:快晴
  - 風景:広々とした風景
  - 物体:快適さをイメージさせる物体

<img width="701" alt="image" src="https://github.com/Takumi1209/AvatarSystemProject/assets/87661689/902984ab-2edf-4691-a5e5-319368247513">

