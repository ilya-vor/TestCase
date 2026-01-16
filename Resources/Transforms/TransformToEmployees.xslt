<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

	<xsl:template match="/">
		<xsl:choose>
			<xsl:when test="Pay[item]">
				<xsl:apply-templates select="Pay" mode="format1"/>
			</xsl:when>
			<xsl:when test="Pay[not (item)]">
				<xsl:apply-templates select="Pay" mode="format2"/>
			</xsl:when>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="Pay" mode="format1">
		<Employees>
			<xsl:for-each select="item[not(@name = preceding::item/@name and @surname = preceding::item/@surname)]">
				<xsl:variable name="currentName" select="@name"/>
				<xsl:variable name="currentSurname" select="@surname"/>

				<Employee name="{$currentName}" surname="{$currentSurname}">
					<xsl:for-each select="../item[@name = $currentName and @surname = $currentSurname]">
						<xsl:sort select="@mount"/>
						<salary amount="{@amount}" mount="{@mount}"/>
					</xsl:for-each>
				</Employee>
			</xsl:for-each>
		</Employees>
	</xsl:template>

	<xsl:template match="Pay" mode="format2">
		<Employees>
			<xsl:variable name="allItems" select="*/item"/>

			<xsl:for-each select="$allItems[
                not(@name = preceding::item/@name and @surname = preceding::item/@surname)
            ]">
				<xsl:variable name="currentName" select="@name"/>
				<xsl:variable name="currentSurname" select="@surname"/>

				<Employee name="{$currentName}" surname="{$currentSurname}">
					<xsl:for-each select="$allItems[@name = $currentName and @surname = $currentSurname]">
						<xsl:sort select="@mount"/>
						<salary amount="{@amount}" mount="{@mount}"/>
					</xsl:for-each>
				</Employee>
			</xsl:for-each>
		</Employees>
	</xsl:template>
</xsl:stylesheet>
